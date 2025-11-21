using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("DANHMUCTENDONVI")]
public class DanhMucKhachHang : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("STT")]
    public int? Stt { get; set; }

    [Column("CAPDT")]
    public int? CapDT { get; set; }

    [Column("MADONVITRUCTHUOC")]
    public string? MaDonViTrucThuoc { get; set; }

    [Column("MADONVI")]
    public string MaDonVi { get; set; }

    [Column("TENDONVI")]
    public string? TenDonVi { get; set; }

    [Column("DIACHI")]
    public string? DiaChi { get; set; }

    [Column("DIENTHOAI")]
    public string? DienThoai { get; set; }

    [Column("EMAIL")]
    public string? Email { get; set; }

    [Column("FAX")]
    public string? Fax { get; set; }

    [Column("MASOTHUE")]
    public string? MaSoThue { get; set; }

    [Column("WEBSITE")]
    public string? Website { get; set; }

    [Column("SOTAIKHOAN")]
    public string? SoTaiKhoan { get; set; }

    [Column("NOIMOTAIKHOAN")]
    public string? NoiMoTaiKhoan { get; set; }

    [Column("SOTAIKHOANNT")]
    public string? SoTaiKhoanNT { get; set; }

    [Column("TRUCTHUOC")]
    public bool? TrucThuoc { get; set; }

    [Column("SELECTTED")]
    public bool? Selected { get; set; }

    [Column("THEODOIHOADON")]
    public bool? TheoDoiHoaDon { get; set; }

    [Column("NGANHANG")]
    public bool? NganHang { get; set; }

    [Column("NHANVIEN")]
    public bool? NhanVien { get; set; }

    [Column("NHACUNGCAP")]
    public bool? NhaCungCap { get; set; }

    [Column("KHACHHANG")]
    public bool? KhachHang { get; set; }

    [Column("KHAC")]
    public bool? Khac { get; set; }

    [Column("MANHOM")]
    public string? MaNhom { get; set; }

    [Column("TENNHOM")]
    public string? TenNhom { get; set; }

    [Column("MALOAIDN")]
    public string? MaLoaiDN { get; set; }

    [Column("TENLOAIDN")]
    public string? TenLoaiDN { get; set; }

    [Column("MAKHUVUC")]
    public string? MaKhuVuc { get; set; }

    [Column("TENKHUVUC")]
    public string? TenKhuVuc { get; set; }

    [Column("HANMUCTHANHTOAN")]
    public int? HangMucThanhToan { get; set; }

    [Column("HANMUCDUNO")]
    public decimal? HanMucDuNo { get; set; }

    [Column("SHTKHACHTOAN")]
    public string? SHTKhachToan { get; set; }

    [Column("NHOMHANGCHINH")]
    public string? NhomHangChinh { get; set; }

    [Column("DAILY")]
    public bool? DaiLy { get; set; }

    [Column("DAILYCAP")]
    public int? DaiLyCap { get; set; }

    [Column("LOAIDONGIA")]
    public string? LoaiDongGia { get; set; }

    [Column("GHICHU")]
    public string? GhiChu { get; set; }

    [Column("LUONGNGAY")]
    public double? LuongNgay { get; set; }

    [Column("CHUCVU")]
    public string? ChucVu { get; set; }

    [Column("NGAYTAO")]
    public DateTime? NgayTao { get; set; }

    [Column("MADONVISUDUNG")]
    public string? MaDonViSuDung { get; set; }

    [Column("TENDONVISUDUNG")]
    public string? TenDonViSuDung { get; set; }

    [Column("LOAIDONVI")]
    public string? LoaiDonVi { get; set; }

    [Column("NHAPHANPHOI")]
    public bool? NhaPhanPhoi { get; set; }

    [Column("CHIETKHAU3LOP")]
    public double? ChietKhau3Lop { get; set; }

    [Column("CHIETKHAU1LOP")]
    public double? ChietKhau1Lop { get; set; }

    [Column("MANHAPHANPHOI")]
    public string? MaNhaPhanPhoi { get; set; }

    [Column("TENNHAPHANPHOI")]
    public string? TenNhaPhanPhoi { get; set; }

    [Column("SELECTTEDCK")]
    public bool? SelectedCK { get; set; }

    [Column("THEODOI")]
    public bool? TheoDoi { get; set; }

    [Column("CHINHSACH")]
    public bool? ChinhSach { get; set; }

    [Column("CHINHSACHGIA")]
    public string? ChinhSachGia { get; set; }

    [Column("MAQUANLY")]
    public string? MaQuanLy { get; set; }

    [Column("TENQUANLY")]
    public string? TenQuanLy { get; set; }

    [Column("CAPDL")]
    public int? CapDL { get; set; }

    [Column("MATINH")]
    public string? MaTinh { get; set; }

    [Column("TENTINH")]
    public string? TenTinh { get; set; }
}
