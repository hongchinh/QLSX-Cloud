# Phân tích vấn đề: Mã đơn vị không hiển thị trên UI khi sửa phiếu

## Vấn đề

Khi sửa phiếu xuất/nhập, trường **"Mã đơn vị"** không hiển thị giá trị trên UI, mặc dù `nhapxuat.MaDonVi` có giá trị trong database.

## Phân tích code hiện tại

### 1. UI Component (dòng 206-223):
```razor
<MudAutocomplete T="DanhMucKhachHangModel" 
                 Value="@GetSelectedKhachHang()"
                 ... />
```

### 2. Method GetSelectedKhachHang() (dòng 1045-1057):
```csharp
private DanhMucKhachHangModel GetSelectedKhachHang()
{
    if (selectedKhachHang != null && !string.IsNullOrEmpty(nhapxuat.MaDonVi))
    {
        // Nếu selected khách hàng có mã khớp với nhapxuat.MaDonVi, trả về
        if (selectedKhachHang.MaDonVi == nhapxuat.MaDonVi)
        {
            return selectedKhachHang;
        }
    }
    
    return null; // ❌ VẤN ĐỀ: Trả về null khi selectedKhachHang = null
}
```

### 3. Khi load dữ liệu ban đầu:
- `SuaClickCallBack()` (dòng 1549): Load `nhapxuat` từ database nhưng **không load `selectedKhachHang`**
- `OnAfterRenderAsync()` (dòng 1282): Cũng không load `selectedKhachHang`

## Nguyên nhân

1. **Khi load dữ liệu ban đầu (edit mode)**:
   - `nhapxuat.MaDonVi` có giá trị (từ database)
   - `selectedKhachHang` = null (chưa được load)
   - `GetSelectedKhachHang()` trả về null vì `selectedKhachHang == null`
   - MudAutocomplete không hiển thị vì `Value = null`

2. **Logic hiện tại**:
   - `GetSelectedKhachHang()` chỉ trả về `selectedKhachHang` nếu đã được set
   - Không tự động load từ `nhapxuat.MaDonVi`

## Giải pháp đề xuất

### **Phương án 1: Load selectedKhachHang khi có MaDonVi (Recommended)** ⭐⭐⭐⭐⭐

#### Mô tả:
- Khi có `nhapxuat.MaDonVi` nhưng `selectedKhachHang` = null, tự động load từ API
- Cập nhật `GetSelectedKhachHang()` để async và tự động load
- Hoặc load trong `OnAfterRenderAsync` hoặc `SuaClickCallBack`

#### Implementation:

**Option A: Load trong OnAfterRenderAsync (Đơn giản nhất)**
```csharp
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
    {
        // ... existing code ...
        
        // THÊM: Load selectedKhachHang nếu có MaDonVi
        if (!string.IsNullOrEmpty(nhapxuat.MaDonVi) && selectedKhachHang == null)
        {
            var dmKH = await dmkhachhangService.GetByCodeAsync(nhapxuat.MaDonVi);
            if (dmKH != null && dmKH.Id > 0)
            {
                selectedKhachHang = dmKH;
            }
        }
        
        // ... existing code ...
    }
    await base.OnAfterRenderAsync(firstRender);
}
```

**Option B: Load trong SuaClickCallBack**
```csharp
private async Task SuaClickCallBack(bool isRead)
{
    if (isRead)
    {
        // ... existing code ...
        
        // THÊM: Load selectedKhachHang nếu có MaDonVi
        if (!string.IsNullOrEmpty(nhapxuat.MaDonVi))
        {
            var dmKH = await dmkhachhangService.GetByCodeAsync(nhapxuat.MaDonVi);
            if (dmKH != null && dmKH.Id > 0)
            {
                selectedKhachHang = dmKH;
            }
        }
        
        // ... existing code ...
    }
    IsReadOnly = isRead;
    StateHasChanged();
}
```

**Option C: Cải thiện GetSelectedKhachHang() để tự động load (Phức tạp hơn)**
- Cần chuyển thành async method
- Cần cache để tránh load nhiều lần
- Phức tạp hơn nhưng tự động hơn

---

### **Phương án 2: Sử dụng Text thay vì Value** ⭐⭐⭐

#### Mô tả:
- Thay vì dùng `Value` (cần object), dùng `Text` để hiển thị `MaDonVi` trực tiếp
- Vẫn giữ logic search và selection

#### Nhược điểm:
- ⚠️ Mất tính năng autocomplete với object
- ⚠️ Cần xử lý thêm khi chọn

---

## Khuyến nghị

**Chọn Phương án 1 - Option A hoặc B** vì:
1. ✅ Đơn giản, dễ implement
2. ✅ Không thay đổi logic hiện tại nhiều
3. ✅ Tự động load khi cần
4. ✅ Giữ nguyên tính năng autocomplete

**Ưu tiên Option A** (load trong OnAfterRenderAsync) vì:
- Load một lần khi form được render
- Áp dụng cho cả create và edit mode
- Không cần thay đổi nhiều code

---

## Các file cần sửa

1. ✅ `QLSX-Web/Pages/NhapXuats/NhapXuatsForm.razor`
   - Thêm logic load `selectedKhachHang` trong `OnAfterRenderAsync` hoặc `SuaClickCallBack`

---

## Lưu ý

1. **Performance**: Chỉ load khi `selectedKhachHang == null` và có `MaDonVi`
2. **Error Handling**: Xử lý trường hợp không tìm thấy khách hàng
3. **StateHasChanged**: Đảm bảo gọi sau khi load để update UI
