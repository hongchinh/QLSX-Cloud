# Phân tích vấn đề: Cột Số lượng tồn không hiển thị chính xác

## Các vấn đề đã phát hiện

### 1. Khi load dữ liệu từ đơn đặt hàng
**Vị trí:** 
- `FindDonHang()` - dòng 1335
- `DonHangKeyDown()` - dòng 1351  
- `OpenDialogDonDatHang()` - dòng 1384

**Vấn đề:** Khi thêm các dòng từ đơn đặt hàng, `SoLuongTon` không được tính toán lại.

**Code hiện tại:**
```csharp
// Trong FindDonHang và DonHangKeyDown
foreach (var item in donhang.NoiDungDonDatHangs)
{
    nhapxuat.NoiDungNhapXuats.Add(mapper.Map<NoiDungNhapXuatModel>(item));
    // THIẾU: Cập nhật SoLuongTon
}
```

### 2. Khi thay đổi kho hàng
**Vị trí:** Dòng 265 - `@bind-Value="@nhapxuat.MaKho"`

**Vấn đề:** Không có event handler để cập nhật lại `SoLuongTon` cho tất cả các dòng khi kho hàng thay đổi.

**Code hiện tại:**
```razor
<MudSelect @bind-Value="@nhapxuat.MaKho" ... />
<!-- THIẾU: Event handler để cập nhật SoLuongTon -->
```

### 3. Khi thay đổi ngày chứng từ
**Vị trí:** Dòng 156 - `@bind-Date="nhapxuat.NgayCT"`

**Vấn đề:** Không có event handler để cập nhật lại `SoLuongTon` khi ngày chứng từ thay đổi.

**Code hiện tại:**
```razor
<MudDatePicker @bind-Date="nhapxuat.NgayCT" ... />
<!-- THIẾU: Event handler để cập nhật SoLuongTon -->
```

### 4. Khi load dữ liệu ban đầu (edit mode)
**Vấn đề:** Khi form được load với dữ liệu có sẵn (ví dụ khi edit), `SoLuongTon` có thể đã được lưu trong database nhưng không chính xác nếu kho hàng hoặc ngày đã thay đổi.

## Giải pháp đề xuất

### Giải pháp 1: Thêm method để cập nhật SoLuongTon cho tất cả các dòng
```csharp
private async Task UpdateSoLuongTonForAllRows()
{
    if (nhapxuat.NoiDungNhapXuats == null || !nhapxuat.NoiDungNhapXuats.Any())
        return;
    
    foreach (var noiDung in nhapxuat.NoiDungNhapXuats)
    {
        if (!string.IsNullOrEmpty(noiDung.MaHangHoa) && !string.IsNullOrEmpty(nhapxuat.MaKho))
        {
            noiDung.SoLuongTon = await GetSoLuongTonTheoCode(
                noiDung.MaHangHoa, 
                nhapxuat.MaKho, 
                nhapxuat.NgayCT ?? DateTime.Now
            );
        }
    }
    StateHasChanged();
}
```

### Giải pháp 2: Thêm event handler cho MaKho thay đổi
```razor
<MudSelect @bind-Value="@nhapxuat.MaKho" 
           ValueChanged="@(async (string value) => { 
               nhapxuat.MaKho = value; 
               await UpdateSoLuongTonForAllRows(); 
           })" 
           ... />
```

### Giải pháp 3: Thêm event handler cho NgayCT thay đổi
```razor
<MudDatePicker @bind-Date="nhapxuat.NgayCT" 
               DateChanged="@(async (DateTime? value) => { 
                   nhapxuat.NgayCT = value; 
                   await UpdateSoLuongTonForAllRows(); 
               })" 
               ... />
```

### Giải pháp 4: Cập nhật SoLuongTon khi load dữ liệu từ đơn đặt hàng
```csharp
// Trong FindDonHang, DonHangKeyDown, OpenDialogDonDatHang
foreach (var item in donhang.NoiDungDonDatHangs)
{
    var ndnx = mapper.Map<NoiDungNhapXuatModel>(item);
    ndnx.Id = 0;
    nhapxuat.NoiDungNhapXuats.Add(ndnx);
    
    // THÊM: Cập nhật SoLuongTon
    if (!string.IsNullOrEmpty(ndnx.MaHangHoa) && !string.IsNullOrEmpty(nhapxuat.MaKho))
    {
        ndnx.SoLuongTon = await GetSoLuongTonTheoCode(
            ndnx.MaHangHoa, 
            nhapxuat.MaKho, 
            nhapxuat.NgayCT ?? DateTime.Now
        );
    }
}
```

### Giải pháp 5: Cập nhật SoLuongTon khi load dữ liệu ban đầu
```csharp
// Trong OnAfterRenderAsync hoặc SuaClickCallBack
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
    {
        // ... existing code ...
        
        // THÊM: Cập nhật SoLuongTon cho tất cả các dòng
        await UpdateSoLuongTonForAllRows();
    }
    await base.OnAfterRenderAsync(firstRender);
}
```

## Lưu ý
- Cần đảm bảo `nhapxuat.MaKho` và `nhapxuat.NgayCT` đã có giá trị trước khi gọi `GetSoLuongTonTheoCode`
- Có thể cần thêm loading indicator khi cập nhật số lượng tồn cho nhiều dòng
- Nên xử lý exception khi gọi API để tránh crash
