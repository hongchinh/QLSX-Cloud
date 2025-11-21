using QLSX.Shared.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Models;

public partial class SettingModel : BaseModel
{
    public SettingModel()
    {
    }

    public SettingModel(Settings entity)
    {
        Id = entity.Id;
        IDID = entity.IdId;
        MADONVICAPTREN = entity.MaDonViCapTren;
        TENDONVICAPTREN = entity.TenDonViCapTren;
        MaDonVi = entity.MaDonVi;
        TenDonVi = entity.TenDonVi;
        DiaChi = entity.DiaChi;
        CHUCDANHKETOAN = entity.ChucDanhKeToan;
        CHUCDANHLAPBIEU = entity.ChucDanhLapBieu;
        CHUCDANHTHUTRUONG = entity.ChucDanhThuTruong;
        HOTENTHUTRUONG = entity.HoTenThuTruong;
        NGAYTHANGLB = entity.NgayThangLB;
        HOTENNGUOILAPBIEU = entity.HoTenNguoiLapBieu;
        HOTENKETOAN = entity.HoTenKeToan;
        HOTENTHUQUY = entity.HoTenThuQuy;
        MASOTHUE = entity.MaSoThue;
        SOTAIKHOAN = entity.SoTaiKhoan;
        TENNGANHANG = entity.TenNganHang;
        DienThoai = entity.DienThoai;
        QUANHUYEN = entity.QuanHuyen;
        Email = entity.Email;
        FAX = entity.Fax;
        TINHTHANHPHO = entity.TinhThanhPho;
        GHICHU = entity.GhiChu;
        WEBSITE = entity.WebSite;
        CHUCDANHTHUKHO = entity.ChucDanhThuKho;
        HOTENTHUKHO = entity.HoTenThuKho;
        NOIDUNGNGHE = entity.NoiDungNghe;
        nganhnghe = entity.NganhNghe;
        UserId = (int)entity.UserId;
        PathImage = entity.PathImage;
        PathLogoImage = entity.PathLogoImage;
        NgayBatDauSuDung = entity.NgayBatDauSuDung;
        LoaiNhapXuat = (int)entity.LoaiNhapXuat;
        isNhapTheoM2 = (bool)entity.IsNhapTheoM2;
        InPhieuSauThemMoi = (bool)entity.InPhieuSauThemMoi;
        TuDongThuChi = (bool)entity.TuDongThuChi;
        TuDongNhapXuat = (bool)entity.TuDongNhapXuat;
        TuDongDonDatHang = (bool)entity.TuDongDonDatHang;
        TuDongMaHangHoa = (bool)entity.TuDongMaHangHoa;
        TuDongMaDonVi = (bool)entity.TuDongMaDonVi;
        TemplateQR = entity.TemplateQR;
        URLQR = entity.UrlQR;
        BankQR = entity.BankQR;
    }

    public int Id { get; set; }
    public string? IDID { get; set; }
    public string? MADONVICAPTREN { get; set; }
    public string? TENDONVICAPTREN { get; set; }
    public string? MaDonVi { get; set; }
    public string? TenDonVi { get; set; }
    public string? DiaChi { get; set; }
    public string? CHUCDANHKETOAN { get; set; }
    public string? CHUCDANHLAPBIEU { get; set; }
    public string? CHUCDANHTHUTRUONG { get; set; }
    public string? HOTENTHUTRUONG { get; set; }
    public string? NGAYTHANGLB { get; set; }
    public string? HOTENNGUOILAPBIEU { get; set; }
    public string? HOTENKETOAN { get; set; }
    public string? HOTENTHUQUY { get; set; }
    public string? MASOTHUE { get; set; }
    public string? SOTAIKHOAN { get; set; }
    public string? TENNGANHANG { get; set; }
    public string? DienThoai { get; set; }
    public string? QUANHUYEN { get; set; }
    public string? Email { get; set; }
    public string? FAX { get; set; }
    public string? TINHTHANHPHO { get; set; }
    public string? GHICHU { get; set; }
    public string? WEBSITE { get; set; }
    public string? CHUCDANHTHUKHO { get; set; }
    public string? HOTENTHUKHO { get; set; }
    public string? NOIDUNGNGHE { get; set; }
    public string? nganhnghe { get; set; }
    public int UserId { get; set; }
    public string? PathImage { get; set; }
    public string? PathLogoImage { get; set; }
    public DateTime? NgayBatDauSuDung { get; set; }
    [NotMapped]
    public string LogoImage { get; set; }
    [NotMapped] public string Image { get; set; }

    //1- nhập thông thường
    //2 - nhập theo m2/m3
    //3- nhập cả 2
    public int LoaiNhapXuat { get; set; }

    public bool isNhapTheoM2 { get; set; }
    public bool InPhieuSauThemMoi { get; set; }
    public bool TuDongThuChi { get; set; }
    public bool TuDongNhapXuat { get; set; }
    public bool TuDongDonDatHang { get; set; }

    public bool TuDongMaHangHoa { get; set; }
    public bool TuDongMaDonVi { get; set; }
    public UserModel User { get; set; }

    public string? TemplateQR { get; set; }
    public string? URLQR { get; set; }
    public string? BankQR { get; set; }
}
