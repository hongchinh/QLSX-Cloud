using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("NOIDUNGNHAPXUATTRANO")]
public class NoiDungNhapXuatTraNo : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("IDID")]
    public int? IdId { get; set; }

    [Column("LOAIPHIEU")]
    public string? LoaiPhieu { get; set; }

    [Column("SHTK")]
    public string? SHTK { get; set; }

    [Column("MAHANGHOA")]
    public string? MaHangHoa { get; set; }

    [Column("TENHANGHOA")]
    public string? TenHangHoa { get; set; }

    [Column("SOLUONGTON")]
    public double? SoLuongTon { get; set; }

    [Column("SOLUONG")]
    public double? SoLuong { get; set; }

    [Column("DONGIA")]
    public double? DonGia { get; set; }

    [Column("DONVITINH")]
    public string? DonViTinh { get; set; }

    [Column("SOTIEN")]
    public double? SoTien { get; set; }

    [Column("SOTIENUSD")]
    public double? SoTienUSD { get; set; }

    [Column("DONGIAUSD")]
    public double? DonGiaUSD { get; set; }

    [Column("MADONVI")]
    public string? MaDonVi { get; set; }

    [Column("TENDONVI")]
    public string? TenDonVi { get; set; }

    [Column("MADONVI1")]
    public string? MaDonVi1 { get; set; }

    [Column("TENDONVI1")]
    public string? TenDonVi1 { get; set; }

    [Column("HANSUDUNG")]
    public DateTime? HanSuDung { get; set; }

    [Column("KETCHUYEN1")]
    public string? KetChuyen1 { get; set; }

    [Column("GIAVON")]
    public double? GiaVon { get; set; }

    [Column("GIAVONUSD")]
    public double? GiaVonUSD { get; set; }

    [Column("MAPHANBO")]
    public string? MaPhanBo { get; set; }

    [Column("TENPHANBO")]
    public string? TenPhanBo { get; set; }

    [Column("COMMENT")]
    public string? Comment { get; set; }

    [Column("CAPNHATGIAVON")]
    public bool? CapNhatGiaVon { get; set; }

    [Column("TYLECHIETKHAU")]
    public double? TyLeChietKhau { get; set; }

    [Column("SOTIENCHIETKHAU")]
    public double? SoTienChietKhau { get; set; }

    [Column("SOTIENXUAT")]
    public double? SoTienXuat { get; set; }

    [Column("DONGIAXUAT")]
    public double? DonGiaXuat { get; set; }

    [Column("CHENHLECH")]
    public double? ChenhLech { get; set; }

    [Column("XUATXU")]
    public string? XuatXu { get; set; }

    [Column("QUYCACH")]
    public string? QuyCach { get; set; }

    [Column("MANHOM")]
    public string? MaNhom { get; set; }

    [Column("MALOAI")]
    public string? MaLoai { get; set; }

    [Column("TENNHOM")]
    public string? TenNhom { get; set; }

    [Column("TENLOAI")]
    public string? TenLoai { get; set; }

    [Column("PHUONGTHUC")]
    public string? PhuongThuc { get; set; }

    [Column("DIENGIAI")]
    public string? DienGiai { get; set; }

    [Column("TONGCHIEUDAI")]
    public double? TongChieuDai { get; set; }

    [Column("TONGDIENTICH")]
    public double? TongDienTich { get; set; }

    [Column("KHORONGTON")]
    public double? KhoRongTon { get; set; }

    [Column("CHIEUDAI")]
    public double? ChieuDai { get; set; }

    [Column("MAHANGHOA1")]
    public string? MaHangHoa1 { get; set; }

    [Column("TENHANGHOA1")]
    public string? TenHangHoa1 { get; set; }

    [Column("DONVITINH1")]
    public string? DonViTinh1 { get; set; }

    [Column("SOLUONG1")]
    public double? SoLuong1 { get; set; }

    [Column("THUENK")]
    public double? ThueNK { get; set; }

    [Column("THUEVAT")]
    public double? ThueVAT { get; set; }

    [Column("TYLEPHIVANCHUYEN")]
    public double? TyLePhiVanChuyen { get; set; }

    [Column("SOTIENPHIVANCHUYEN")]
    public double? SoTienPhiVanChuyen { get; set; }

    [Column("TYLEKHUYENMAI")]
    public double? TyLeKhuyenMai { get; set; }

    [Column("SOTIENKHUYENMAI")]
    public double? SoTienKhuyenMai { get; set; }

    [Column("TONGCONG")]
    public double? TongCong { get; set; }

    [Column("TINHCHAT")]
    public string? TinhChat { get; set; }

    [Column("KIEU")]
    public string? Kieu { get; set; }

    [Column("SOTIENVND")]
    public double? SoTienVND { get; set; }

    [Column("POLY")]
    public double? POLY { get; set; }

    [Column("MDI")]
    public double? MDI { get; set; }

    [Column("MAHOACHATPOLY")]
    public string? MaHoaChatPoly { get; set; }

    [Column("MAHOACHATMDI")]
    public string? MaHoaChatMDI { get; set; }

    [Column("MADONVINHAN")]
    public string? MaDonViNhan { get; set; }

    [Column("TENDONVINHAN")]
    public string? TenDonViNhan { get; set; }

    [Column("NUOCSANXUAT")]
    public string? NuocSanXuat { get; set; }

    [Column("TYLECK")]
    public double? TyLeCK { get; set; }

    [Column("SOTIENOK")]
    public double? SoTienOK { get; set; }

    [Column("DONGIABAN")]
    public double? DonGiaBan { get; set; }

    [Column("SOTIENBAN")]
    public double? SoTienBan { get; set; }

    [Column("MAKHO")]
    public string? MaKho { get; set; }

    [Column("THOIGIANTAO")]
    public string? ThoiGianTao { get; set; }

    [Column("SOLUONGTRA")]
    public double? SoLuongTra { get; set; }

    [Column("LOHANG")]
    public string? LoHang { get; set; }

    [Column("MABARCODE")]
    public string? MaBarCode { get; set; }

    [Column("DONGIABANLE")]
    public double? DonGiaBanLe { get; set; }

    [Column("SOTIENBANLE")]
    public double? SoTienBanLe { get; set; }

    [Column("TYLEVAT")]
    public double? TyLeVAT { get; set; }

    [Column("SOTIENVAT")]
    public double? SoTienVAT { get; set; }

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

    [Column("TYTRONG")]
    public double? TyTrong { get; set; }

    [Column("DONGIADVT1")]
    public double? DonGiaDVT1 { get; set; }

    [Column("SOLUONGDVT1")]
    public double? SoLuongDVT1 { get; set; }

    [Column("SOTIENDVT1")]
    public double? SoTienDVT1 { get; set; }

    [Column("TYLE_CK_NV")]
    public double? TyLeCkNV { get; set; }

    [Column("SOTIEN_CK_NV")]
    public double? SoTienCkNv { get; set; }

    [Column("SOPHIEULSX")]
    public string? SoPhieuLSX { get; set; }

    [Column("NGAYPHIEULSX")]
    public DateTime? NgayPhieuLSX { get; set; }

    [Column("TRANGTHAI_DETAIL")]
    public int? TrangThaiDetail { get; set; }

    [Column("SOPHIEUXUAT")]
    public string? SoPhieuXuat { get; set; }

    [Column("NGAYXUATKHO")]
    public DateTime? NgayXuatKho { get; set; }

    [Column("NGAYXACNHAN")]
    public DateTime? NgayXacNhan { get; set; }

    [Column("NhapXuatId")]
    public int? NhapXuatId { get; set; }
}
