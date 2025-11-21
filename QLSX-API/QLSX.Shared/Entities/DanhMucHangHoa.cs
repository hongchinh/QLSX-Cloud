using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("DANHMUCHANGHOA")]
public class DanhMucHangHoa : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("STT")]
    public int? Stt { get; set; }

    [Column("MAHANGHOA")]
    public string MaHangHoa { get; set; }

    [Column("TENHANGHOA")]
    public string? TenHangHoa { get; set; }

    [Column("DONVITINH")]
    public string? DonViTinh { get; set; }

    [Column("DONVITINH1")]
    public string? DonViTinh1 { get; set; }

    [Column("GIANHAP")]
    public decimal? GiaNhap { get; set; }

    [Column("GIAXUAT")]
    public decimal? GiaXuat { get; set; }

    [Column("GHICHU")]
    public string? GhiChu { get; set; }

    [Column("SELECTTED")]
    public bool? Selected { get; set; }

    [Column("TYLE")]
    public decimal? TyLe { get; set; }

    [Column("SOLUONGQUYDOI")]
    public double? SoLuongQuyDoi { get; set; }

    [Column("QUYCACH")]
    public string? QuyCach { get; set; }

    [Column("XUATXU")]
    public string? XuatXu { get; set; }

    [Column("MAUHANGHOA")]
    public string? MauHangHoa { get; set; }

    [Column("MANHOMHANG")]
    public string? MaNhomHang { get; set; }

    [Column("TENNHOMHANG")]
    public string? TenNhomHang { get; set; }

    [Column("DONGIA")]
    public decimal? DonGia { get; set; }

    [Column("SOLUONGA")]
    public decimal? SoLuongA { get; set; }

    [Column("SOLUONGTONA")]
    public decimal? SoLuongTonA { get; set; }

    [Column("TRUYENTHONG")]
    public bool? TruyenThong { get; set; }

    [Column("MAPHANBO")]
    public string? MaPhanBo { get; set; }

    [Column("THUTU")]
    public int? ThuTu { get; set; }

    [Column("HANGHOA")]
    public bool? HangHoa { get; set; }

    [Column("TYLEVAY")]
    public double? TyLeVay { get; set; }

    [Column("TYLEVAT")]
    public double? TyLeVAT { get; set; }

    [Column("GIABANLE")]
    public double? GiaBanLe { get; set; }

    [Column("NGUYENLIEU")]
    public bool? NguyenLieu { get; set; }

    [Column("TYLEGIAMGIA")]
    public double? TyLeGiamGia { get; set; }

    [Column("TYLECHIETKHAU")]
    public double? TyLeChietKhau { get; set; }

    [Column("IMAGES")]
    public string? Images { get; set; }

    [Column("NGAYTAO")]
    public DateTime? NgayTao { get; set; }

    [Column("MADONVISUDUNG")]
    public string? MaDonViSuDung { get; set; }

    [Column("TENDONVISUDUNG")]
    public string? TenDonViSuDung { get; set; }

    [Column("GIASAIGON")]
    public double? GiaSaiGon { get; set; }

    [Column("NGAYSANXUAT")]
    public DateTime? NgaySanXuat { get; set; }

    [Column("HANSUDUNG")]
    public DateTime? HanSuDung { get; set; }

    [Column("NAMSANXUAT")]
    public string? NamSanXuat { get; set; }

    [Column("LOHANG")]
    public string? LoHang { get; set; }

    [Column("TYLEKHUYENMAI")]
    public double? TyLeKhuyenMai { get; set; }

    [Column("HESOQUYDOI")]
    public decimal? HeSoQuyDoi { get; set; }

    [Column("NGUONGTHONGBAO")]
    public double? NguoiThongBao { get; set; }

    [Column("PHUONGTHUC")]
    public string? PhuongThuc { get; set; }

    [Column("TRONGLUONG")]
    public double? TrongLuong { get; set; }

    [Column("TYLETRON")]
    public double? TyLeTron { get; set; }

    [Column("TYLETIEUTHU")]
    public double? TyLeTieuThu { get; set; }

    [Column("THUONGXUYEN")]
    public bool? ThuongXuyen { get; set; }

    [Column("NGUONGTHONGBAOMAX")]
    public double? NguongThongBaoMax { get; set; }

    [Column("NGUONGTHONGBAO1")]
    public double? NguongThongBao1 { get; set; }

    [Column("THIETYEU")]
    public bool? ThietYeu { get; set; }

    [Column("DINHKY")]
    public bool? DinhKy { get; set; }

    [Column("MABARCODE")]
    public string? MaBarCode { get; set; }

    [Column("MANHACC")]
    public string? MaNhaCC { get; set; }

    [Column("TENNHACC")]
    public string? TenNhaCC { get; set; }

    [Column("SELECTMAVACH")]
    public bool? SelectMaVach { get; set; }

    [Column("SOLUONGMAVACH")]
    public double? SoLuongMaVach { get; set; }

    [Column("NAMXUATBAN")]
    public string? NamXuatBan { get; set; }

    [Column("MAHANGHOACU")]
    public string? MaHangHoaCu { get; set; }

    [Column("CHIETKHAU")]
    public double? ChietKhau { get; set; }

    [Column("TENNHACUNGCAP")]
    public string? TenNhaCungCap { get; set; }

    [Column("TACGIA")]
    public string? TacGia { get; set; }

    [Column("SOLUONGTOITHIEU")]
    public double? SoLuongToiThieu { get; set; }

    [Column("SOLUONGTOIDA")]
    public double? SoLuongToiDa { get; set; }

    [Column("GIABIA")]
    public double? GiaBia { get; set; }

    [Column("TYLECHIETKHAU1")]
    public double? TyLeChietKhau1 { get; set; }

    [Column("QUYDOI")]
    public bool? QuyDoi { get; set; }

    [Column("MASOQUYDOI")]
    public string? MaSoQuyDoi { get; set; }

    [Column("LOAITHUE")]
    public string? LoaiThue { get; set; }

    [Column("LOAIHANG")]
    public string? LoaiHang { get; set; }

    [Column("LOAITON")]
    public string? LoaiTon { get; set; }

    [Column("MAUSAC")]
    public string? MauSac { get; set; }

    [Column("DODAY")]
    public string? DoDay { get; set; }

    [Column("KIEUSONG")]
    public string? KieuSong { get; set; }

    [Column("CHUNGLOAI")]
    public string? ChungLoai { get; set; }

    [Column("MALOAITON")]
    public string? MaLoaiTon { get; set; }

    [Column("MAMAUSAC")]
    public string? MaMauSac { get; set; }

    [Column("MADODAY")]
    public string? MaDoDay { get; set; }

    [Column("MAKIEUSONG")]
    public string? MaKieuSong { get; set; }

    [Column("MACHUNGLOAI")]
    public string? MaChungLoai { get; set; }

    [Column("KHORONGTON")]
    public double? KhoRongTon { get; set; }

    [Column("TYTRONG")]
    public double? TyTrong { get; set; }

    [Column("GIAXUAT1")]
    public double? GiaXuat1 { get; set; }

    [Column("GIABANLE1")]
    public double? GiaBanLe1 { get; set; }

    [Column("TONKHO")]
    public bool? TonKho { get; set; }
}
