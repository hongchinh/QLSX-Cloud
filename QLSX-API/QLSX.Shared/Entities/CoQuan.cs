using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("COQUAN")]
public class CoQuan : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("IDID")]
    public string? IdId { get; set; }

    [Column("MADONVI")]
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
    public string? FAX { get; set; }

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
}
