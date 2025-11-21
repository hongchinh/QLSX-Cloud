using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("CHUNGTU")]

public class ThuChi : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }
    
    [Column("STT")]
    public int? Stt { get; set; }
    
    [Column("LOAI")]
    public string? Loai { get; set; }
    
    [Column("LOAIPHIEU")]
    public string? LoaiPhieu { get; set; }
    
    [Column("MADOITUONG")]
    public string? MaDoiTuong { get; set; }
    
    [Column("TENDOITUONG")]
    public string? TenDoiTuong { get; set; }
    
    [Column("DIACHI")]
    public string? DiaChi { get; set; }
    
    [Column("PHIEU")]
    public string? Phieu { get; set; }
    
    [Column("NGAYCT")]
    public DateTime? NgayCT { get; set; }
    
    [Column("SOCHUNGTU")]
    public string? SoChungTu { get; set; }
    
    [Column("SELECTEDIN")]
    public bool? SelectedIN { get; set; }
    
    [Column("USERNAME")]
    public string? UserName { get; set; }
    
    [Column("CAPNHATVON")]
    public string? CapNhatVon { get; set; }
    
    [Column("DIENGIAI")]
    public string? DienGiai { get; set; }
    
    [Column("LOAITIEN")]
    public string? LoaiTien { get; set; }
    
    [Column("TYGIA")]
    public double? TyGia { get; set; }
    
    [Column("DATHANHTOAN")]
    public bool? DaThanhToan { get; set; }
    
    [Column("PHIEUTHUCHI")]
    public string? PhieuThuChi { get; set; }
    
    [Column("DANOPQUY")]
    public bool? DaNopQuy { get; set; }
    
    [Column("NGAYTHANHTOAN")]
    public DateTime? NgayThanhToan { get; set; }
    
    [Column("LYDOTHUCHI")]
    public string? LyDoThuChi { get; set; }
    
    [Column("MALYDO")]
    public string? MaLyDo { get; set; }
    
    [Column("SOTIENVND")]
    public double? SoTienVND { get; set; }
    
    [Column("SOTIENUSD")]
    public double? SoTienUSD { get; set; }
    
    [Column("MAKHOANCHI")]
    public string? MaKhoanChi { get; set; }
    
    [Column("TENKHOANCHI")]
    public string? TenKhoanChi { get; set; }
    
    [Column("MAKHOANTHU")]
    public string? MaKhoanThu { get; set; }
    
    [Column("TENKHOANTHU")]
    public string? TenKhoanThu { get; set; }
    
    [Column("MADONVISUDUNG")]
    public string? MaDonViSuDung { get; set; }
    
    [Column("TENDONVISUDUNG")]
    public string? TenDonViSuDung { get; set; }
    
    [Column("GHICHU")]
    public string? GhiChu { get; set; }
    
    [Column("IDTHUCHI")]
    public string? IdThuChi { get; set; }
    
    [Column("IMG_QRCODE")]
    public byte[] IMG_QRCODE { get; set; }

    [Column("SODONHANG")] 
    public string? SoDonHang { get; set; }
}
