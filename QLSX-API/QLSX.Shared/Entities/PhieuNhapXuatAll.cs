using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

public class PhieuNhapXuatAll : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("LOAI")]
    public string? Loai { get; set; }

    [Column("LOAIPHIEU")]
    public string? LoaiPhieu { get; set; }

    [Column("STT")]
    public string? Stt { get; set; }

    [Column("PHIEU")]
    public string? Phieu { get; set; }

    [Column("NGAYCT")]
    public DateTime? NgayCT { get; set; }

    [Column("NGAYGHI")]
    public DateTime? NgayGhi { get; set; }

    [Column("SOCHUNGTU")]
    public string? SoChungTu { get; set; }

    [Column("DIENGIAI")]
    public string? DienGiai { get; set; }

    [Column("TONGCONG")]
    public double? TongCong { get; set; }

    [Column("MADOITUONG")]
    public string? MaDoiTuong { get; set; }

    [Column("TENDOITUONG")]
    public string? TenDoiTuong { get; set; }

    [Column("DIACHIDOITUONG")]
    public string? DiaChiDoiTuong { get; set; }

    [Column("KEMTHEO")]
    public string? KemTheo { get; set; }

    [Column("SELECTTED")]
    public bool? Selected { get; set; }

    [Column("USERNAME")]
    public string? UserName { get; set; }

    [Column("GHICHU")]
    public string? GhiChu { get; set; }

    [Column("MAKHO")]
    public string? MaKho { get; set; }

    [Column("TENKHO")]
    public string? TenKho { get; set; }

    [Column("MALYDO")]
    public string? MaLyDo { get; set; }

    [Column("TENLYDO")]
    public string? TenLyDo { get; set; }

    [Column("LOAITIEN")]
    public string? LoaiTien { get; set; }

    [Column("TYGIA")]
    public double? TyGia { get; set; }

    [Column("SOHOADON")]
    public string? SoHoaDon { get; set; }

    [Column("TYLEVATTMP")]
    public string? TyLeVATTMP { get; set; }

    [Column("MASOTHUE")]
    public string? MaSoThue { get; set; }

    [Column("HANTHANHTOAN")]
    public DateTime? HanThanhToan { get; set; }

    [Column("TYLEVAT")]
    public double? TyLeVAT { get; set; }

    [Column("LOAINHAPXUAT")]
    public string? LoaiNhapXuat { get; set; }

    [Column("TOA")]
    public string? Toa { get; set; }

    [Column("IDID")]
    public int IdId { get; set; }

    [Column("LPHIEU")]
    public string? LPhieu { get; set; }

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

    [Column("THANHTOAN")]
    public bool? ThanhToan { get; set; }

    [Column("SOTIENTTVND")]
    public double? SoTienTTVND { get; set; }

    [Column("SOTIENTTUSD")]
    public double? SoTienTTUSD { get; set; }

    [Column("MADONVISUDUNG")]
    public string? MaDonViSuDung { get; set; }

    [Column("TENDONVISUDUNG")]
    public string? TenDonViSuDung { get; set; }

    [Column("TYLECHIETKHAU")]
    public double? TyLeChietKhau { get; set; }

    [Column("SOTIENCHIETKHAU")]
    public double? SoTienChietKhau { get; set; }

    [Column("SOTIENXUAT")]
    public double? SoTienXuat { get; set; }

    [Column("DONGIAXUAT")]
    public double? DonGiaXuat { get; set; }

    [Column("PHIVANCHUYEN")]
    public double? PhiVanChuyen { get; set; }

    [Column("CHENHLECH")]
    public double? ChenhLech { get; set; }

    [Column("XUATXU")]
    public string? XuatXu { get; set; }

    [Column("QUYCACH")]
    public string? QuyCach { get; set; }

    [Column("SOPHIEUYEUCAU")]
    public string? SoPhieuYeuCau { get; set; }

    [Column("CHONIN")]
    public bool? ChonIn { get; set; }

    [Column("DUAN")]
    public bool? DuAn { get; set; }

    [Column("HINHTHUCTT")]
    public string? HinhThucTT { get; set; }

    [Column("NGAYHOPDONG")]
    public DateTime? NgayHopDong { get; set; }

    [Column("SOHOPDONG")]
    public string? SoHopDong { get; set; }

    [Column("NOIGIAOHANG")]
    public string? NoiGiaoHang { get; set; }

    [Column("THOIGIANGIAOHANG")]
    public string? ThoiGianGiaoHang { get; set; }

    [Column("TEAMOK")]
    public string? TeamOK { get; set; }

    [Column("SOTIENOK")]
    public double? SoTienOK { get; set; }

    [Column("TYLECK")]
    public double? TyLeCK { get; set; }

    [Column("SOTIENTT")]
    public double? SoTienTT { get; set; }

    [Column("CHIETKHAU")]
    public bool? ChietKhau { get; set; }

    [Column("TENKHO1")]
    public string? TenKho1 { get; set; }

    [Column("MaKho1")]
    public string? MAKHO1 { get; set; }

    [Column("TENNGUOINHAN")]
    public string? TenNguoiThan { get; set; }

    [Column("MANGUOINHAN")]
    public string? MaNguoiThan { get; set; }

    [Column("MANGUOICHUYEN")]
    public string? MaNguoiChuyen { get; set; }

    [Column("TENNGUOICHUYEN")]
    public string? TenNguoiChuyen { get; set; }

    [Column("SOTIENBANLE")]
    public double? SoTienBanLe { get; set; }

    [Column("DONGIABANLE")]
    public double? DonGiaBanLe { get; set; }

    [Column("MABARCODE")]
    public string? MaBarCode { get; set; }

    [Column("SOTIENVAT")]
    public double? SoTienVAT { get; set; }

    [Column("TYLEVATOK")]
    public double? TyLeVATOK { get; set; }

    [Column("SOTIENVATOK")]
    public double? SoTienVATOK { get; set; }

    [Column("CHIEUDAI")]
    public double? ChieuDai { get; set; }

    [Column("KHORONGTON")]
    public double? KhoRongTon { get; set; }

    [Column("TONGDIENTICH")]
    public double? TongDienTich { get; set; }

    [Column("TONGCHIEUDAI")]
    public double? TongChieuDai { get; set; }

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

    [Column("DIADIEM")]
    public string? DiaDiem { get; set; }

    [Column("THOIGIANGIAO")]
    public string? ThoiGianGiao { get; set; }

    [Column("NGUOIQL")]
    public string? NguoiQL { get; set; }

    [Column("NGAYGIAO")]
    public DateTime? NgayGiao { get; set; }

    [Column("MAQUANLY")]
    public string? MaQuanLy { get; set; }

    [Column("TENQUANLY")]
    public string? TenQuanLy { get; set; }

    [Column("TYLE_CK_NV")]
    public double? TyLeCkNv { get; set; }

    [Column("SOTIEN_CK_NV")]
    public double? SoTienCkNv { get; set; }

    [Column("IMG_QRCODE")]
    public byte? ImgQrCode { get; set; }

    [Column("SOPHIEUXUAT")]
    public string? SoPhieuXuat { get; set; }

    [Column("SOPHIEULSX")]
    public string? SoPhieuLSX { get; set; }

    [Column("LOHANG")]
    public string? LoHang { get; set; }

    [Column("TRANGTHAI_DETAIL")]
    public int? TrangThaiDetail { get; set; }

    [Column("NGAYXACNHAN")]
    public DateTime? NgayXacNhan { get; set; }

    [Column("NGAYHUY")]
    public DateTime? NgayHuy { get; set; }

    [Column("TIENHANG")]
    public double? TienHang { get; set; }

    [Column("HINHTHUCGIAOHANG")]
    public string? HinhThucGiaoHang { get; set; }

    [Column("TRANGTHAI")]
    public int? TrangThai { get; set; }

    [Column("MATINH")]
    public string? MaTinh { get; set; }

    [Column("TENTINH")]
    public string? TenTinh { get; set; }

    [Column("CAPDL")]
    public int? CapDL { get; set; }

    [Column("SOTIENCK")]
    public double? SoTienCK { get; set; }

    [Column("DIACHIQUANLY")]
    public string? DiaChiQuanLy { get; set; }

    [Column("SOTIEN_GIAM")]
    public double? SoTienGiam { get; set; }

    [Column("MANHOM")]
    public string? MaNhom { get; set; }

    [Column("NGAYPHIEULSX")]
    public DateTime? NgayPhieuLSX { get; set; }

    [Column("NGAYXUATKHO")]
    public DateTime? NgayXuatKho { get; set; }
}
