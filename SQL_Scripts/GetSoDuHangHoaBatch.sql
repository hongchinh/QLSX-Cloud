-- =============================================
-- Stored Procedure: GetSoDuHangHoaBatch
-- Mô tả: Tính số lượng tồn cho nhiều hàng hóa cùng lúc (Batch)
-- Tối ưu: Giảm số lượng round-trip đến database
-- =============================================

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSoDuHangHoaBatch]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetSoDuHangHoaBatch]
GO

CREATE PROCEDURE [dbo].[GetSoDuHangHoaBatch]
    @MaKho NVARCHAR(50),
    @MaHangHoasXML NVARCHAR(MAX), -- XML chứa danh sách mã hàng hóa: <items><item>MAHH1</item><item>MAHH2</item></items>
    @Ngay DATETIME,
    @MaDonViSuDung NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Tạo bảng tạm để chứa danh sách mã hàng hóa
    DECLARE @MaHangHoaTable TABLE (MaHangHoa NVARCHAR(50));
    
    -- Parse XML và insert vào bảng tạm
    DECLARE @XMLDoc XML;
    SET @XMLDoc = CAST(@MaHangHoasXML AS XML);
    
    INSERT INTO @MaHangHoaTable (MaHangHoa)
    SELECT 
        T.c.value('.', 'NVARCHAR(50)') AS MaHangHoa
    FROM @XMLDoc.nodes('/items/item') T(c);
    
    -- Tính số lượng tồn cho từng hàng hóa
    -- Giả sử stored procedure GetSoDuHangHoa hiện tại có logic tính tồn kho
    -- Chúng ta sẽ gọi logic tương tự nhưng cho nhiều hàng hóa
    
    -- Tạo bảng kết quả
    DECLARE @ResultTable TABLE (
        MaHangHoa NVARCHAR(50),
        SoLuong FLOAT
    );
    
    -- Tính số lượng tồn cho từng hàng hóa
    -- Lưu ý: Logic này cần được điều chỉnh dựa trên stored procedure GetSoDuHangHoa hiện tại
    -- Đây là ví dụ, bạn cần thay thế bằng logic thực tế từ GetSoDuHangHoa
    
    INSERT INTO @ResultTable (MaHangHoa, SoLuong)
    SELECT 
        mhh.MaHangHoa,
        ISNULL(SUM(
            CASE 
                WHEN nx.Loai = 'nhap' THEN nd.SoLuong
                WHEN nx.Loai = 'xuat' THEN -nd.SoLuong
                WHEN nx.Loai = 'dieuchuyen' AND nx.MaKho = @MaKho THEN -nd.SoLuong
                WHEN nx.Loai = 'dieuchuyen' AND nx.MaKhoDen = @MaKho THEN nd.SoLuong
                ELSE 0
            END
        ), 0) AS SoLuong
    FROM @MaHangHoaTable mhh
    LEFT JOIN NoiDungNhapXuats nd ON nd.MaHangHoa = mhh.MaHangHoa
    LEFT JOIN NhapXuats nx ON nx.loaiphieu = nd.loaiphieu
        --AND nx.madonvisudung = @MaDonViSuDung
        AND nx.NgayCT <= @Ngay
        AND (nx.DeletedDate IS NULL)
        AND (
            (@MaKho = '' or nx.MaKho = @MaKho)
        )
    WHERE (nd.DeletedDate IS NULL)
    GROUP BY mhh.MaHangHoa;
    
    -- Trả về kết quả
    SELECT MaHangHoa, SoLuong 
    FROM @ResultTable;
    
    -- Nếu không có kết quả nào, trả về 0 cho tất cả mã hàng hóa
    -- Đảm bảo tất cả mã hàng hóa trong request đều có kết quả
    INSERT INTO @ResultTable (MaHangHoa, SoLuong)
    SELECT mhh.MaHangHoa, 0
    FROM @MaHangHoaTable mhh
    WHERE NOT EXISTS (
        SELECT 1 FROM @ResultTable rt WHERE rt.MaHangHoa = mhh.MaHangHoa
    );
    
    SELECT MaHangHoa, SoLuong 
    FROM @ResultTable;
END
GO

-- =============================================
-- Lưu ý: 
-- 1. Stored procedure này là ví dụ, bạn cần điều chỉnh logic tính tồn kho
--    dựa trên stored procedure GetSoDuHangHoa hiện tại của bạn
-- 2. Có thể cần điều chỉnh tên bảng và cột cho phù hợp với database thực tế
-- 3. Nếu có stored procedure GetSoDuHangHoa hiện tại, có thể refactor để tái sử dụng logic
-- =============================================
