using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("NHAPXUATTONCUON")]
public class NhapXuatTonCuon : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("LOAI")]
    public string? Loai { get; set; }

    [Column("LOAIPHIEU")]
    public string? LoaiPhieu { get; set; }

    [Column("STT")]
    public int? STT { get; set; }

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
    public string? TyLeVATMP { get; set; }

    [Column("MASOTHUE")]
    public string? MaSoThue { get; set; }

    [Column("HANTHANHTOAN")]
    public DateTime? HanThanhToan { get; set; }

    [Column("TYLEVAT")]
    public double? TyLeVAT { get; set; }

    [Column("LOAINHAPXUAT")]
    public string? LoaiNhapXuat { get; set; }

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

    [Column("TOA")]
    public string? Toa { get; set; }

    [Column("PHIVANCHUYEN")]
    public double? PhiVanChuyen { get; set; }

    [Column("SOPHIEUYEUCAU")]
    public string? SoPhieuYeuCau { get; set; }

    [Column("TEAMOK")]
    public string? TeamOK { get; set; }

    [Column("THOIGIANGIAOHANG")]
    public string? ThoiGianGiaoHang { get; set; }

    [Column("NOIGIAOHANG")]
    public string? NoiGiaoHang { get; set; }

    [Column("SOHOPDONG")]
    public string? SoHopDong { get; set; }

    [Column("NGAYHOPDONG")]
    public DateTime? NgayHopDong { get; set; }

    [Column("HINHTHUCTT")]
    public string? HinhThucTT { get; set; }

    [Column("DUAN")]
    public bool? DuAn { get; set; }

    [Column("CHONIN")]
    public bool? ChonIn { get; set; }

    [Column("SOTIENTT")]
    public double? SoTienTT { get; set; }

    [Column("MANGUOICHUYEN")]
    public string? MaNguoiChuyen { get; set; }

    [Column("TENNGUOICHUYEN")]
    public string? TenNguoiChuyen { get; set; }

    [Column("MANGUOINHAN")]
    public string? MaNguoiNhan { get; set; }

    [Column("TENNGUOINHAN")]
    public string? TenNguoiNhan { get; set; }

    [Column("MAKHO1")]
    public string? MaKho1 { get; set; }

    [Column("TENKHO1")]
    public string? TenKho1 { get; set; }

    [Column("CHIETKHAU")]
    public bool? ChietKhau { get; set; }

    [Column("SOTIENVAT")]
    public double? SoTienVAT { get; set; }

    [Column("CHUYEN")]
    public bool? Chuyen { get; set; }

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

    [Column("NGUOIQL")]
    public string? NguoiQL { get; set; }

    [Column("DIADIEM")]
    public string? DiaDiem { get; set; }

    [Column("THOIGIANGIAO")]
    public string? ThoiGianGiao { get; set; }

    [Column("NGAYGIAO")]
    public DateTime? NgayGiao { get; set; }

    [Column("SOTIENCK")]
    public double? SoTienCK { get; set; }

    [Column("DIACHIQUANLY")]
    public string? DiaChiQuanLy { get; set; }

    [Column("IMG_QRCODE")]
    public byte[] ImgQrCode { get; set; }

    [Column("TRANGTHAI")]
    public int? TrangThai { get; set; }

    [Column("HINHTHUCGIAOHANG")]
    public string? HinhThucGiaoHang { get; set; }

    [Column("TIENHANG")]
    public double? TienHang { get; set; }

    [Column("NGAYHUY")]
    public DateTime? NgayHuy { get; set; }

    [Column("SOTIEN_GIAM")] 
    public double? SoTienGiam { get; set; }

    public virtual List<NoiDungNhapXuatTonCuon> NoiDungNhapXuatTonCuons { get; set; }
}
