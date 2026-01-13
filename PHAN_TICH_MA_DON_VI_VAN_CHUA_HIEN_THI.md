# Phân tích: Mã đơn vị vẫn chưa hiển thị khi sửa

## Vấn đề

Mã đơn vị vẫn chưa hiển thị khi vào form nhập xuất để sửa, mặc dù đã đổi sang property binding.

## Phân tích flow load dữ liệu

### 1. Flow khi mở form sửa:

**NhapXuatEdit.razor (Parent Component):**
```
OnAfterRenderAsync(firstRender = true)
  ↓
Load nhapxuat từ API (dòng 131-132)
  ↓
nhapxuat = mapper.Map<NhapXuatModel>(nhapxuatnew)
  ↓
StateHasChanged() (dòng 134)
  ↓
Render NhapXuatsForm component với nhapxuat đã có dữ liệu
```

**NhapXuatsForm.razor (Child Component):**
```
OnAfterRenderAsync(firstRender = true) (dòng 1297)
  ↓
Check: nhapxuat.MaDonVi có giá trị? (dòng 1312)
  ↓
Load selectedKhachHang từ API (dòng 1316-1319)
  ↓
selectedKhachHang = dmKH (dòng 1319)
  ↓
StateHasChanged() (dòng 1333) - NHƯNG không có InvokeAsync!
```

### 2. Vấn đề phát hiện:

#### Vấn đề 1: Thiếu InvokeAsync trong OnAfterRenderAsync
- Dòng 1319: `selectedKhachHang = dmKH;` 
- Dòng 1333: `StateHasChanged();` - **KHÔNG có InvokeAsync!**
- Trong `OnAfterRenderAsync`, cần dùng `InvokeAsync(StateHasChanged)` để đảm bảo thread-safe

#### Vấn đề 2: Timing issue
- `OnAfterRenderAsync` được gọi sau khi component đã render
- MudAutocomplete có thể đã render với `selectedKhachHang = null`
- Sau đó `selectedKhachHang` được set nhưng MudAutocomplete không tự động cập nhật nếu không có `StateHasChanged()` đúng cách

#### Vấn đề 3: SuaClickCallBack có thể không được gọi
- `SuaClickCallBack` chỉ được gọi khi user click nút "Sửa"
- Nhưng khi mở form sửa từ `NhapXuatEdit`, form đã ở chế độ read-only (`IsReadOnly="true"`)
- Có thể `SuaClickCallBack` không được gọi khi mở form lần đầu

## Giải pháp đề xuất

### **Phương án 1: Sửa OnAfterRenderAsync để dùng InvokeAsync** ⭐⭐⭐⭐⭐

#### Mô tả:
- Đổi `StateHasChanged()` thành `await InvokeAsync(StateHasChanged)` trong `OnAfterRenderAsync`
- Đảm bảo thread-safe và force update UI

#### Implementation:

```csharp
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
    {
        // ... existing code ...
        
        // Load selectedKhachHang nếu có MaDonVi nhưng chưa được load
        if (!string.IsNullOrEmpty(nhapxuat.MaDonVi) && selectedKhachHang == null)
        {
            try
            {
                var dmKH = await dmkhachhangService.GetByCodeAsync(nhapxuat.MaDonVi);
                if (dmKH != null && dmKH.Id > 0)
                {
                    selectedKhachHang = dmKH;
                    // Force update UI sau khi load
                    await InvokeAsync(StateHasChanged);  // ✅ Thêm InvokeAsync
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading selectedKhachHang: {ex.Message}");
            }
        }
        
        // ... existing code ...
    }
    await base.OnAfterRenderAsync(firstRender);
}
```

---

### **Phương án 2: Load trong OnParametersSetAsync** ⭐⭐⭐⭐

#### Mô tả:
- Load `selectedKhachHang` trong `OnParametersSetAsync` thay vì `OnAfterRenderAsync`
- `OnParametersSetAsync` được gọi khi parameters thay đổi, đảm bảo load đúng timing

#### Implementation:

```csharp
protected override async Task OnParametersSetAsync()
{
    // Load selectedKhachHang khi nhapxuat.MaDonVi thay đổi
    if (!string.IsNullOrEmpty(nhapxuat.MaDonVi))
    {
        // Chỉ load nếu chưa có hoặc không khớp
        if (selectedKhachHang == null || selectedKhachHang.MaDonVi != nhapxuat.MaDonVi)
        {
            try
            {
                var dmKH = await dmkhachhangService.GetByCodeAsync(nhapxuat.MaDonVi);
                if (dmKH != null && dmKH.Id > 0)
                {
                    selectedKhachHang = dmKH;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading selectedKhachHang: {ex.Message}");
            }
        }
    }
    else
    {
        selectedKhachHang = null;
    }
    
    await base.OnParametersSetAsync();
}
```

---

### **Phương án 3: Kết hợp cả hai** ⭐⭐⭐⭐⭐ (Recommended)

#### Mô tả:
- Load trong `OnParametersSetAsync` để đảm bảo load đúng timing
- Giữ logic trong `OnAfterRenderAsync` như một fallback
- Sửa `StateHasChanged()` thành `InvokeAsync(StateHasChanged)`

#### Ưu điểm:
- ✅ Đảm bảo load đúng timing
- ✅ Có fallback nếu `OnParametersSetAsync` không được gọi
- ✅ Thread-safe với `InvokeAsync`

---

## Khuyến nghị

**Chọn Phương án 3** vì:
1. ✅ Đảm bảo load đúng timing với `OnParametersSetAsync`
2. ✅ Có fallback với `OnAfterRenderAsync`
3. ✅ Thread-safe với `InvokeAsync(StateHasChanged)`
4. ✅ Đáng tin cậy nhất

---

## Các file cần sửa

1. ✅ `QLSX-Web/Pages/NhapXuats/NhapXuatsForm.razor`
   - Thêm `OnParametersSetAsync` để load `selectedKhachHang` khi `nhapxuat` thay đổi
   - Sửa `StateHasChanged()` thành `InvokeAsync(StateHasChanged)` trong `OnAfterRenderAsync`

---

## Lưu ý

1. **OnParametersSetAsync**: Được gọi khi parameters thay đổi, đảm bảo load đúng timing
2. **InvokeAsync**: Cần dùng trong async methods để đảm bảo thread-safe
3. **Timing**: Đảm bảo load trước khi MudAutocomplete render
