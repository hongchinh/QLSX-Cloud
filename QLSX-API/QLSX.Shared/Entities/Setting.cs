using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("Settings")]
public class Settings
{
    
    [Column("ID")]
    public int Id { get; set; }
    
    [Column("IDID")]
    public string? IdId { get; set; }
    
    [Column("MaDonVi")]
    public string? MaDonVi { get; set; }
    
    [Column("MADONVICAPTREN")]
    public string? MaDonViCapTren { get; set; }
    
    [Column("TENDONVICAPTREN")]
    public string? TenDonViCapTren { get; set; }
    
    [Column("TENDONVI")]
    public string? TenDonVi { get; set; }
    
    [Column("DIACHI")]
    public string? DiaChi { get; set; }
    
    [Column("CHUCDANHTHUTRUONG")]
    public string? ChucDanhThuTruong { get; set; }
    
    [Column("HOTENTHUTRUONG")]
    public string? HoTenThuTruong { get; set; }
    
    [Column("CHUCDANHKETOAN")]
    public string? ChucDanhKeToan { get; set; }
    
    [Column("CHUCDANHLAPBIEU")]
    public string? ChucDanhLapBieu { get; set; }
    
    [Column("NGAYTHANGLB")]
    public string? NgayThangLB { get; set; }
    
    [Column("HOTENNGUOILAPBIEU")]
    public string? HoTenNguoiLapBieu { get; set; }
    
    [Column("HOTENKETOAN")]
    public string? HoTenKeToan { get; set; }
    
    [Column("HOTENTHUQUY")]
    public string? HoTenThuQuy { get; set; }
    
    [Column("MASOTHUE")]
    public string? MaSoThue { get; set; }
    
    [Column("SOTAIKHOAN")]
    public string? SoTaiKhoan { get; set; }
    
    [Column("TENNGANHANG")]
    public string? TenNganHang { get; set; }
    
    [Column("DIENTHOAI")]
    public string? DienThoai { get; set; }
    
    [Column("QUANHUYEN")]
    public string? QuanHuyen { get; set; }
    
    [Column("EMAIL")]
    public string? Email { get; set; }
    
    [Column("FAX")]
    public string? Fax { get; set; }
    
    [Column("TINHTHANHPHO")]
    public string? TinhThanhPho { get; set; }
    
    [Column("GHICHU")]
    public string? GhiChu { get; set; }
    
    [Column("WEBSITE")]
    public string? WebSite { get; set; }
    
    [Column("CHUCDANHTHUKHO")]
    public string? ChucDanhThuKho { get; set; }
    
    [Column("HOTENTHUKHO")]
    public string? HoTenThuKho { get; set; }
    
    [Column("NOIDUNGNGHE")]
    public string? NoiDungNghe { get; set; }
    
    [Column("nganhnghe")]
    public string? NganhNghe { get; set; }
    
    [Column("UserId")]
    public int? UserId { get; set; }
    
    [Column("DMDonViSuDungId")]
    public int? DMDonViSuDungId { get; set; }
    
    [Column("CreatedDate")]
    public DateTime? CreatedDate { get; set; }
    
    [Column("UpdatedDate")]
    public DateTime? UpdatedDate { get; set; }
    
    [Column("DeletedDate")]
    public DateTime? DeletedDate { get; set; }
    
    [Column("CreateBy")]
    public string? CreateBy { get; set; }
    
    [Column("PathImage")]
    public string? PathImage { get; set; }
    
    [Column("PathLogoImage")]
    public string? PathLogoImage { get; set; }
    
    [Column("LoaiNhapXuat")]
    public int? LoaiNhapXuat { get; set; }
    
    [Column("isNhapTheoM2")]
    public bool? IsNhapTheoM2 { get; set; }
    
    [Column("InPhieuSauThemMoi")]
    public bool? InPhieuSauThemMoi { get; set; }
    
    [Column("UrlReport")]
    public string? UrlReport { get; set; }
    
    [Column("NgayBatDauSuDung")]
    public DateTime? NgayBatDauSuDung { get; set; }
    
    [Column("TuDongThuChi")]
    public bool? TuDongThuChi { get; set; }
    
    [Column("TuDongNhapXuat")]
    public bool? TuDongNhapXuat { get; set; }
    
    [Column("TuDongMaHangHoa")]
    public bool? TuDongMaHangHoa { get; set; }
    
    [Column("TuDongMaDonVi")]
    public bool? TuDongMaDonVi { get; set; }
    
    [Column("TuDongDonDatHang")]
    public bool? TuDongDonDatHang { get; set; }
    
    [Column("TemplateQR")]
    public string? TemplateQR { get; set; }
    
    [Column("URLQR")]
    public string? UrlQR { get; set; }

    [Column("BankQR")] 
    public string? BankQR { get; set; }
}
