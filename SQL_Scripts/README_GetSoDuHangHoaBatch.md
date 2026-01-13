# Hướng dẫn sử dụng Stored Procedure Batch

## Tổng quan

Stored procedure `GetSoDuHangHoaBatch` được tạo để tối ưu việc tính số lượng tồn cho nhiều hàng hóa cùng lúc, thay vì gọi từng stored procedure riêng biệt.

## Cài đặt

1. Mở file `GetSoDuHangHoaBatch.sql` trong SQL Server Management Studio
2. Kiểm tra và điều chỉnh logic tính tồn kho cho phù hợp với database của bạn
3. Chạy script để tạo stored procedure

## Lưu ý quan trọng

### 1. Điều chỉnh logic tính tồn kho

File `GetSoDuHangHoaBatch.sql` hiện tại chỉ là **ví dụ**. Bạn cần:

- Xem stored procedure `GetSoDuHangHoa` hiện tại của bạn
- Copy logic tính tồn kho từ đó
- Điều chỉnh để xử lý nhiều hàng hóa cùng lúc

### 2. Kiểm tra tên bảng và cột

Đảm bảo các tên bảng và cột trong stored procedure khớp với database thực tế:
- `NoiDungNhapXuats`
- `NhapXuats`
- Các cột: `MaHangHoa`, `SoLuong`, `Loai`, `MaKho`, `MaKhoDen`, `NgayCT`, etc.

### 3. Kích hoạt stored procedure batch trong API

Sau khi tạo stored procedure trong database, cập nhật file `DMHangHoasController.cs`:

```csharp
bool useBatchProc = true; // Đổi từ false sang true
```

## Cách hoạt động

### Input:
- `@MaKho`: Mã kho hàng
- `@MaHangHoasXML`: XML chứa danh sách mã hàng hóa
  ```xml
  <items>
    <item>MAHH1</item>
    <item>MAHH2</item>
    <item>MAHH3</item>
  </items>
  ```
- `@Ngay`: Ngày tính tồn kho
- `@DMDonViSuDungId`: ID đơn vị sử dụng

### Output:
Bảng với 2 cột:
- `MaHangHoa`: Mã hàng hóa
- `SoLuong`: Số lượng tồn

## Ví dụ sử dụng

```sql
DECLARE @XML NVARCHAR(MAX) = '<items><item>MAHH1</item><item>MAHH2</item></items>';
EXEC GetSoDuHangHoaBatch 'KHO01', @XML, '2024-01-01', 1;
```

## Tối ưu hóa

Nếu bạn muốn tối ưu hơn nữa, có thể:

1. **Sử dụng Table-Valued Parameter** thay vì XML (nhanh hơn)
2. **Tạo index** trên các cột thường xuyên query:
   - `NhapXuats.MaKho`
   - `NhapXuats.NgayCT`
   - `NoiDungNhapXuats.MaHangHoa`
3. **Cache kết quả** ở application level (đã implement)

## Troubleshooting

### Lỗi: "Invalid object name"
- Kiểm tra tên bảng và schema (có thể cần `dbo.` prefix)

### Lỗi: "Invalid column name"
- Kiểm tra tên cột trong database
- Đảm bảo các cột tồn tại trong bảng

### Kết quả không chính xác
- So sánh với kết quả từ `GetSoDuHangHoa` để đảm bảo logic giống nhau
- Kiểm tra điều kiện WHERE và JOIN
