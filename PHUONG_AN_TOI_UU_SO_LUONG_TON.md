# Phương án tối ưu hiệu suất tính số lượng tồn

## Tình trạng hiện tại

### Vấn đề:
- Mỗi lần cần số lượng tồn → gọi 1 API riêng biệt
- API gọi stored procedure `GetSoDuHangHoa` cho từng hàng hóa
- Nếu có 10 dòng trong phiếu → 10 API calls
- Khi thay đổi kho/ngày → phải gọi lại tất cả API
- **Ảnh hưởng:** Chậm, tốn tài nguyên, trải nghiệm người dùng kém

### Cấu trúc API hiện tại:
```csharp
// Mỗi lần gọi cần:
- MaKho (string)
- MaHangHoa (string)  
- Ngay (DateTime)
- DMDonViSuDungId (TenantId)
```

---

## Phương án đề xuất

### **Phương án 1: Batch API - Gọi một lần cho nhiều hàng hóa** ⭐⭐⭐⭐⭐
**Độ ưu tiên: CAO NHẤT**

#### Mô tả:
Thay vì gọi API từng hàng hóa một, tạo API mới nhận danh sách hàng hóa và trả về số lượng tồn cho tất cả.

#### Ưu điểm:
- ✅ Giảm số lượng API calls từ N xuống 1
- ✅ Tối ưu database (1 stored procedure call thay vì N)
- ✅ Giảm network overhead
- ✅ Cải thiện hiệu suất đáng kể (10x - 100x)

#### Implementation:

**1. Tạo Request Model mới:**
```csharp
// QLSX-API/QLSX.Shared/Models/Request/GetSoDuHangHoaBatchRequest.cs
public class GetSoDuHangHoaBatchRequest : BaseRequest
{
    public string MaKho { get; set; }
    public DateTime Ngay { get; set; }
    public List<string> MaHangHoas { get; set; } // Danh sách mã hàng hóa
}
```

**2. Tạo Response Model:**
```csharp
// QLSX-API/QLSX.Shared/Models/Response/GetSoDuHangHoaBatchResponse.cs
public class GetSoDuHangHoaBatchResponse
{
    public Dictionary<string, double> SoLuongTon { get; set; } 
    // Key: MaHangHoa, Value: SoLuongTon
}
```

**3. Tạo API Endpoint mới:**
```csharp
// QLSX-API/QLSX.API/Controllers/DMHangHoasController.cs
[HttpPost("GetSoDuHangHoaBatch")]
public async Task<ActionResult<GetSoDuHangHoaBatchResponse>> GetSoDuHangHoaBatch(GetSoDuHangHoaBatchRequest request)
{
    try
    {
        var result = new GetSoDuHangHoaBatchResponse 
        { 
            SoLuongTon = new Dictionary<string, double>() 
        };
        
        // Tối ưu: Gọi stored procedure một lần với danh sách hàng hóa
        // Hoặc gọi nhiều lần nhưng trong một transaction
        foreach (var maHangHoa in request.MaHangHoas)
        {
            string StoredProc = string.Format(
                "exec dbo.GetSoDuHangHoa {0}, '{1}', '{2}', '{3}'", 
                request.MaKho, 
                maHangHoa, 
                request.Ngay.ToString("MM/dd/yyyy"), 
                _tenantProvider.TenantId
            );
            
            // Execute và lưu kết quả
            // ... (code tương tự như GetSoDuHangHoaByCode)
            result.SoLuongTon[maHangHoa] = soLuong;
        }
        
        return result;
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

**4. Tối ưu hơn: Tạo stored procedure batch:**
```sql
-- Tạo stored procedure mới trong SQL Server
CREATE PROCEDURE GetSoDuHangHoaBatch
    @MaKho NVARCHAR(50),
    @MaHangHoas NVARCHAR(MAX), -- JSON array hoặc comma-separated
    @Ngay DATETIME,
    @DMDonViSuDungId INT
AS
BEGIN
    -- Xử lý batch trong một query duy nhất
    -- Trả về bảng với MaHangHoa và SoLuong
END
```

**5. Cập nhật Service:**
```csharp
// QLSX-Web/Services/BanTon/DMHangHoaService.cs
public async Task<Dictionary<string, double>> GetSoDuHangHoaBatchAsync(GetSoDuHangHoaBatchRequest request)
{
    request.DMDonViSuDungId = _appService.DMDonViSuDungId;
    string serializedUser = JsonConvert.SerializeObject(request);
    
    var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri + "/GetSoDuHangHoaBatch");
    // ... (code tương tự)
    
    var response = await _httpClient.SendAsync(requestMessage);
    var responseBody = await response.Content.ReadAsStringAsync();
    var result = JsonConvert.DeserializeObject<GetSoDuHangHoaBatchResponse>(responseBody);
    
    return result.SoLuongTon;
}
```

**6. Cập nhật NhapXuatsForm:**
```csharp
// Thay vì gọi từng cái một:
private async Task UpdateSoLuongTonForAllRows()
{
    if (nhapxuat.NoiDungNhapXuats == null || !nhapxuat.NoiDungNhapXuats.Any())
        return;
    
    // Lấy danh sách mã hàng hóa cần tính
    var maHangHoas = nhapxuat.NoiDungNhapXuats
        .Where(x => !string.IsNullOrEmpty(x.MaHangHoa))
        .Select(x => x.MaHangHoa)
        .Distinct()
        .ToList();
    
    if (!maHangHoas.Any() || string.IsNullOrEmpty(nhapxuat.MaKho))
        return;
    
    // GỌI BATCH API - MỘT LẦN DUY NHẤT
    var request = new GetSoDuHangHoaBatchRequest
    {
        MaKho = nhapxuat.MaKho,
        Ngay = nhapxuat.NgayCT ?? DateTime.Now,
        MaHangHoas = maHangHoas
    };
    
    var soLuongTonDict = await dmhanghoaService.GetSoDuHangHoaBatchAsync(request);
    
    // Cập nhật số lượng tồn cho từng dòng
    foreach (var noiDung in nhapxuat.NoiDungNhapXuats)
    {
        if (!string.IsNullOrEmpty(noiDung.MaHangHoa) && 
            soLuongTonDict.ContainsKey(noiDung.MaHangHoa))
        {
            noiDung.SoLuongTon = soLuongTonDict[noiDung.MaHangHoa];
        }
    }
    
    StateHasChanged();
}
```

#### Hiệu quả:
- **Trước:** 10 dòng = 10 API calls = ~2-5 giây
- **Sau:** 10 dòng = 1 API call = ~0.2-0.5 giây
- **Cải thiện:** 10x nhanh hơn

---

### **Phương án 2: Debounce/Throttle API calls** ⭐⭐⭐⭐
**Độ ưu tiên: CAO**

#### Mô tả:
Tránh gọi API quá nhiều khi user đang nhập liệu hoặc thay đổi giá trị.

#### Implementation:
```csharp
private System.Threading.Timer _debounceTimer;
private readonly TimeSpan _debounceDelay = TimeSpan.FromMilliseconds(500);

private async Task UpdateSoLuongTonForAllRowsDebounced()
{
    // Hủy timer cũ nếu có
    _debounceTimer?.Dispose();
    
    // Tạo timer mới
    _debounceTimer = new System.Threading.Timer(async _ =>
    {
        await UpdateSoLuongTonForAllRows();
        _debounceTimer?.Dispose();
        _debounceTimer = null;
    }, null, _debounceDelay, TimeSpan.FromMilliseconds(-1));
}
```

#### Sử dụng:
```csharp
// Khi thay đổi kho hàng
private async Task OnMaKhoChanged(string value)
{
    nhapxuat.MaKho = value;
    await UpdateSoLuongTonForAllRowsDebounced(); // Debounced
}

// Khi thay đổi ngày
private async Task OnNgayCTChanged(DateTime? value)
{
    nhapxuat.NgayCT = value;
    await UpdateSoLuongTonForAllRowsDebounced(); // Debounced
}
```

---

### **Phương án 3: Cache với Smart Invalidation** ⭐⭐⭐
**Độ ưu tiên: TRUNG BÌNH**

#### Mô tả:
Cache số lượng tồn ở client, chỉ refresh khi cần thiết.

#### Implementation:
```csharp
// Cache key: "MaKho_MaHangHoa_Ngay"
private Dictionary<string, (double value, DateTime timestamp)> _soLuongTonCache = new();
private readonly TimeSpan _cacheExpiry = TimeSpan.FromMinutes(5);

private async Task<double> GetSoLuongTonCached(string maHangHoa, string maKho, DateTime ngay)
{
    string cacheKey = $"{maKho}_{maHangHoa}_{ngay:yyyyMMdd}";
    
    // Kiểm tra cache
    if (_soLuongTonCache.ContainsKey(cacheKey))
    {
        var (value, timestamp) = _soLuongTonCache[cacheKey];
        if (DateTime.Now - timestamp < _cacheExpiry)
        {
            return value; // Trả về từ cache
        }
    }
    
    // Không có trong cache hoặc đã hết hạn → gọi API
    var soLuong = await GetSoLuongTonTheoCode(maHangHoa, maKho, ngay);
    
    // Lưu vào cache
    _soLuongTonCache[cacheKey] = (soLuong, DateTime.Now);
    
    return soLuong;
}

// Invalidate cache khi cần
private void InvalidateSoLuongTonCache(string maKho = null, DateTime? ngay = null)
{
    if (maKho != null || ngay != null)
    {
        var keysToRemove = _soLuongTonCache.Keys
            .Where(k => 
                (maKho == null || k.StartsWith($"{maKho}_")) &&
                (ngay == null || k.EndsWith($"_{ngay:yyyyMMdd}"))
            )
            .ToList();
        
        foreach (var key in keysToRemove)
        {
            _soLuongTonCache.Remove(key);
        }
    }
    else
    {
        _soLuongTonCache.Clear(); // Clear all
    }
}
```

---

### **Phương án 4: Lazy Loading - Chỉ load khi cần** ⭐⭐⭐
**Độ ưu tiên: TRUNG BÌNH**

#### Mô tả:
Chỉ load số lượng tồn khi user thực sự cần (ví dụ: khi edit dòng, khi focus vào cột).

#### Implementation:
```csharp
// Chỉ load khi user click vào dòng hoặc edit
private async Task OnPreviewEditClick(object item)
{
    var row = (NoiDungNhapXuatModel)item;
    
    // Chỉ load nếu chưa có hoặc cần refresh
    if (row.SoLuongTon == 0 || _needsRefresh)
    {
        row.SoLuongTon = await GetSoLuongTonTheoCode(
            row.MaHangHoa, 
            nhapxuat.MaKho, 
            nhapxuat.NgayCT ?? DateTime.Now
        );
    }
}
```

---

### **Phương án 5: Background Refresh** ⭐⭐
**Độ ưu tiên: THẤP**

#### Mô tả:
Load số lượng tồn ở background, không block UI.

#### Implementation:
```csharp
private async Task UpdateSoLuongTonInBackground()
{
    // Hiển thị loading indicator
    _isLoadingSoLuongTon = true;
    StateHasChanged();
    
    // Chạy ở background
    _ = Task.Run(async () =>
    {
        await UpdateSoLuongTonForAllRows();
        
        // Cập nhật UI trên main thread
        await InvokeAsync(() =>
        {
            _isLoadingSoLuongTon = false;
            StateHasChanged();
        });
    });
}
```

---

## Kết hợp các phương án (Recommended)

### **Phương án tối ưu nhất: Batch API + Debounce + Cache**

```csharp
// 1. Batch API để giảm số lượng calls
// 2. Debounce để tránh gọi quá nhiều khi user đang nhập
// 3. Cache để tránh gọi lại những giá trị không thay đổi

private Dictionary<string, (double value, DateTime timestamp)> _soLuongTonCache = new();
private System.Threading.Timer _debounceTimer;
private readonly TimeSpan _debounceDelay = TimeSpan.FromMilliseconds(500);
private readonly TimeSpan _cacheExpiry = TimeSpan.FromMinutes(5);

private async Task UpdateSoLuongTonForAllRowsOptimized()
{
    // Debounce
    _debounceTimer?.Dispose();
    _debounceTimer = new System.Threading.Timer(async _ =>
    {
        await UpdateSoLuongTonForAllRowsBatch();
        _debounceTimer?.Dispose();
        _debounceTimer = null;
    }, null, _debounceDelay, TimeSpan.FromMilliseconds(-1));
}

private async Task UpdateSoLuongTonForAllRowsBatch()
{
    if (nhapxuat.NoiDungNhapXuats == null || !nhapxuat.NoiDungNhapXuats.Any())
        return;
    
    // Lấy danh sách cần tính (loại bỏ những cái đã có trong cache)
    var itemsToFetch = new List<string>();
    var now = DateTime.Now;
    
    foreach (var noiDung in nhapxuat.NoiDungNhapXuats)
    {
        if (string.IsNullOrEmpty(noiDung.MaHangHoa) || string.IsNullOrEmpty(nhapxuat.MaKho))
            continue;
            
        string cacheKey = $"{nhapxuat.MaKho}_{noiDung.MaHangHoa}_{(nhapxuat.NgayCT ?? DateTime.Now):yyyyMMdd}";
        
        // Kiểm tra cache
        if (_soLuongTonCache.ContainsKey(cacheKey))
        {
            var (value, timestamp) = _soLuongTonCache[cacheKey];
            if (now - timestamp < _cacheExpiry)
            {
                noiDung.SoLuongTon = value; // Dùng cache
                continue;
            }
        }
        
        itemsToFetch.Add(noiDung.MaHangHoa);
    }
    
    // Chỉ gọi API cho những cái chưa có trong cache
    if (itemsToFetch.Any())
    {
        var request = new GetSoDuHangHoaBatchRequest
        {
            MaKho = nhapxuat.MaKho,
            Ngay = nhapxuat.NgayCT ?? DateTime.Now,
            MaHangHoas = itemsToFetch.Distinct().ToList()
        };
        
        var soLuongTonDict = await dmhanghoaService.GetSoDuHangHoaBatchAsync(request);
        
        // Cập nhật và cache
        foreach (var noiDung in nhapxuat.NoiDungNhapXuats)
        {
            if (string.IsNullOrEmpty(noiDung.MaHangHoa))
                continue;
                
            string cacheKey = $"{nhapxuat.MaKho}_{noiDung.MaHangHoa}_{(nhapxuat.NgayCT ?? DateTime.Now):yyyyMMdd}";
            
            if (soLuongTonDict.ContainsKey(noiDung.MaHangHoa))
            {
                noiDung.SoLuongTon = soLuongTonDict[noiDung.MaHangHoa];
                _soLuongTonCache[cacheKey] = (noiDung.SoLuongTon, now);
            }
        }
    }
    
    StateHasChanged();
}
```

---

## So sánh hiệu suất

| Phương án | Số API calls (10 dòng) | Thời gian ước tính | Độ phức tạp |
|-----------|------------------------|-------------------|-------------|
| **Hiện tại** | 10 | ~2-5 giây | Thấp |
| **Batch API** | 1 | ~0.2-0.5 giây | Trung bình |
| **Batch + Debounce** | 1 (debounced) | ~0.2-0.5 giây | Trung bình |
| **Batch + Debounce + Cache** | 0-1 (tùy cache) | ~0-0.5 giây | Cao |

---

## Khuyến nghị triển khai

### Phase 1 (Ưu tiên cao - Làm ngay):
1. ✅ **Tạo Batch API** - Giảm số lượng calls từ N xuống 1
2. ✅ **Cập nhật NhapXuatsForm** để sử dụng Batch API

### Phase 2 (Ưu tiên trung bình):
3. ✅ **Thêm Debounce** - Tránh gọi quá nhiều khi user đang nhập
4. ✅ **Thêm Cache** - Tối ưu hơn nữa

### Phase 3 (Tùy chọn):
5. ⚠️ **Lazy Loading** - Nếu cần
6. ⚠️ **Background Refresh** - Nếu cần

---

## Lưu ý

1. **Stored Procedure:** Nên tối ưu stored procedure để xử lý batch hiệu quả hơn
2. **Error Handling:** Cần xử lý lỗi khi một số hàng hóa không tìm thấy
3. **Loading Indicator:** Hiển thị loading khi đang tính số lượng tồn
4. **Validation:** Đảm bảo MaKho và NgayCT có giá trị trước khi gọi API
