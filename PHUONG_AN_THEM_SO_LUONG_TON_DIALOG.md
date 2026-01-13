# Phương án thêm cột số lượng tồn vào DialogHangHoa

## Phân tích hiện trạng

### DialogHangHoa hiện tại:
- **File**: `QLSX-Web/Pages/Dialogs/DialogHangHoa.razor`
- **Cấu trúc**: MudTable với ServerData (paging, sorting, filtering)
- **Các cột hiện tại**:
  - Mã Hàng Hóa
  - Tên hàng hóa
  - DVT
  - Giá nhập
  - Giá xuất

### Cách gọi từ NhapXuatsForm:
```csharp
var options = new DialogOptions { ... };
var result = await DialogService.Show<DialogHangHoa>("Danh sách hàng hóa", options).Result;
```
- **Hiện tại**: Không truyền parameters nào

## Yêu cầu

Thêm cột **"SL Tồn"** (Số lượng tồn) vào DialogHangHoa để user có thể xem số lượng tồn khi chọn hàng hóa.

## Phương án đề xuất

### **Phương án 1: Tính số lượng tồn cho từng trang (Recommended)** ⭐⭐⭐⭐⭐

#### Mô tả:
- Truyền `MaKho` và `NgayCT` từ NhapXuatsForm vào Dialog qua DialogParameters
- Sau khi load danh sách hàng hóa trong mỗi trang, gọi Batch API để tính số lượng tồn
- Lưu kết quả vào Dictionary để hiển thị
- Chỉ tính cho hàng hóa trong trang hiện tại (tối ưu hiệu suất)

#### Ưu điểm:
- ✅ Hiệu suất tốt (chỉ tính cho trang hiện tại)
- ✅ Sử dụng Batch API (đã có sẵn)
- ✅ Không ảnh hưởng đến các form khác sử dụng DialogHangHoa (optional parameters)
- ✅ Có thể cache kết quả trong session dialog

#### Nhược điểm:
- ⚠️ Khi chuyển trang, cần tính lại (nhưng nhanh với Batch API)

#### Implementation:

**1. Cập nhật NhapXuatsForm - Truyền parameters:**
```csharp
async Task OpenDialogHangHoa()
{
    var options = new DialogOptions { CloseOnEscapeKey = true, CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true };
    
    // THÊM: Truyền MaKho và NgayCT vào Dialog
    var parameters = new DialogParameters
    {
        ["MaKho"] = nhapxuat.MaKho ?? "",
        ["NgayCT"] = nhapxuat.NgayCT ?? DateTime.Now,
        ["ShowSoLuongTon"] = true // Flag để biết có hiển thị cột số lượng tồn không
    };
    
    var result = await DialogService.Show<DialogHangHoa>("Danh sách hàng hóa", parameters, options).Result;
    // ... existing code ...
}
```

**2. Cập nhật DialogHangHoa - Nhận parameters và tính số lượng tồn:**
```csharp
@code {
    // THÊM: Parameters để tính số lượng tồn
    [Parameter] public string MaKho { get; set; }
    [Parameter] public DateTime? NgayCT { get; set; }
    [Parameter] public bool ShowSoLuongTon { get; set; } = false;
    
    // THÊM: Dictionary để lưu số lượng tồn
    private Dictionary<string, double> _soLuongTonCache = new Dictionary<string, double>();
    private bool _isLoadingSoLuongTon = false;
    
    // THÊM: Inject service
    @inject IDMHangHoaService<DanhMucHangHoaModel> dmhanghoaService
    @inject AppService AppService
    
    // THÊM: Method tính số lượng tồn cho danh sách hàng hóa hiện tại
    private async Task LoadSoLuongTonForCurrentPage(IEnumerable<DanhMucHangHoaModel> items)
    {
        if (!ShowSoLuongTon || string.IsNullOrEmpty(MaKho) || NgayCT == null)
            return;
            
        var maHangHoas = items
            .Where(x => !string.IsNullOrEmpty(x.MaHangHoa))
            .Select(x => x.MaHangHoa)
            .Distinct()
            .ToList();
            
        if (!maHangHoas.Any())
            return;
            
        _isLoadingSoLuongTon = true;
        try
        {
            var request = new GetSoDuHangHoaBatchRequest
            {
                MaKho = MaKho,
                Ngay = NgayCT.Value,
                MaHangHoas = maHangHoas
            };
            
            var result = await dmhanghoaService.GetSoDuHangHoaBatchAsync(request);
            
            // Lưu vào cache
            foreach (var kvp in result)
            {
                _soLuongTonCache[kvp.Key] = kvp.Value;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading SoLuongTon: {ex.Message}");
        }
        finally
        {
            _isLoadingSoLuongTon = false;
        }
    }
    
    // CẬP NHẬT: ServerReload để tính số lượng tồn sau khi load data
    private async Task<TableData<DanhMucHangHoaModel>> ServerReload(TableState state)
    {
        Loading = true;
        // ... existing code ...
        
        lsResponses = await productServicece.GetAllPagedDialogAsync(itemRequest);
        IEnumerable<DanhMucHangHoaModel> data = lsResponses.Items;
        // ... existing code ...
        
        // THÊM: Tính số lượng tồn cho trang hiện tại
        if (ShowSoLuongTon && data != null && data.Any())
        {
            await LoadSoLuongTonForCurrentPage(data);
        }
        
        Loading = false;
        return new TableData<DanhMucHangHoaModel>() { TotalItems = totalItems, Items = pagedData };
    }
}
```

**3. Cập nhật UI - Thêm cột "SL Tồn":**
```razor
<HeaderContent>
    <MudTh>Mã Hàng Hóa</MudTh>
    <MudTh>Tên hàng hóa</MudTh>
    <MudTh>DVT</MudTh>
    <MudTh>Giá nhập</MudTh>
    <MudTh>Giá xuất</MudTh>
    @if (ShowSoLuongTon)
    {
        <MudTh>SL Tồn</MudTh>
    }
</HeaderContent>
<RowTemplate>
    <!-- ... existing columns ... -->
    @if (ShowSoLuongTon)
    {
        <MudTd DataLabel="SoLuongTon">
            <div @onclick:stopPropagation="true"
                 @ondblclick="@( (x) => DoSelectedRow(context))">
                @if (_isLoadingSoLuongTon)
                {
                    <MudProgressCircular Size="Size.Small" Indeterminate="true" />
                }
                else if (_soLuongTonCache.ContainsKey(context.MaHangHoa))
                {
                    @_soLuongTonCache[context.MaHangHoa].ToString("###,##0.##")
                }
                else
                {
                    <span>-</span>
                }
            </div>
        </MudTd>
    }
</RowTemplate>
```

---

### **Phương án 2: Tính số lượng tồn cho tất cả hàng hóa (Không khuyến nghị)** ⭐⭐

#### Mô tả:
- Tính số lượng tồn cho tất cả hàng hóa trong database khi mở dialog
- Lưu vào Dictionary lớn

#### Nhược điểm:
- ❌ Hiệu suất kém (tính cho tất cả hàng hóa)
- ❌ Tốn tài nguyên
- ❌ Chậm khi mở dialog

---

## So sánh phương án

| Tiêu chí | Phương án 1 | Phương án 2 |
|----------|-------------|-------------|
| **Hiệu suất** | ⭐⭐⭐⭐⭐ | ⭐⭐ |
| **Tốc độ load** | Nhanh | Chậm |
| **Tài nguyên** | Tối ưu | Tốn kém |
| **Trải nghiệm** | Tốt | Kém |
| **Khả năng mở rộng** | Cao | Thấp |

---

## Khuyến nghị

**Chọn Phương án 1** vì:
1. ✅ Hiệu suất tốt nhất
2. ✅ Sử dụng Batch API đã có sẵn
3. ✅ Không ảnh hưởng đến các form khác
4. ✅ Dễ maintain và mở rộng

---

## Lưu ý

1. **Optional Parameters**: `MaKho`, `NgayCT`, `ShowSoLuongTon` là optional, nên các form khác vẫn có thể dùng DialogHangHoa bình thường
2. **Cache**: Có thể cache kết quả trong session dialog để tránh tính lại khi chuyển trang rồi quay lại
3. **Error Handling**: Cần xử lý lỗi khi API fail (hiển thị "-" hoặc "N/A")
4. **Loading Indicator**: Hiển thị loading khi đang tính số lượng tồn

---

## Các file cần sửa

1. ✅ `QLSX-Web/Pages/NhapXuats/NhapXuatsForm.razor` - Truyền parameters
2. ✅ `QLSX-Web/Pages/Dialogs/DialogHangHoa.razor` - Thêm cột và logic tính số lượng tồn

---

## Kết quả mong đợi

- User mở DialogHangHoa từ NhapXuatsForm
- Thấy cột "SL Tồn" với số lượng tồn của từng hàng hóa
- Số lượng tồn được tính dựa trên `MaKho` và `NgayCT` từ form
- Khi chuyển trang, số lượng tồn được tính lại cho trang mới
- Hiển thị loading indicator khi đang tính
