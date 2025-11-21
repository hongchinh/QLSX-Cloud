using QLSX.Shared.Entities;
using System;

namespace QLSX.Shared.Models;

public class PhieuNhapXuatAllModel
{
    public PhieuNhapXuatAllModel()
    {
    }

    public PhieuNhapXuatAllModel(PhieuNhapXuatAll entity)
    {
        Id = entity.Id;
        Loai = entity.Loai;
        LoaiPhieu = entity.LoaiPhieu;
        Stt = entity.Stt;
        Phieu = entity.Phieu;
        NgayCT = entity.NgayCT;
        NgayGhi = entity.NgayGhi;
        SoChungTu = entity.SoChungTu;
        DienGiai = entity.DienGiai;
        TongCong = entity.TongCong;
        MaDoiTuong = entity.MaDoiTuong;
        TenDoiTuong = entity.TenDoiTuong;
        DiaChiDoiTuong = entity.DiaChiDoiTuong;
        KemTheo = entity.KemTheo;
        Selected = entity.Selected;
        UserName = entity.UserName;
        GhiChu = entity.GhiChu;
        MaKho = entity.MaKho;
        TenKho = entity.TenKho;
        MaLyDo = entity.MaLyDo;
        TenLyDo = entity.TenLyDo;
        LoaiTien = entity.LoaiTien;
        TyGia = entity.TyGia;
        SoHoaDon = entity.SoHoaDon;
        TyLeVATTMP = entity.TyLeVATTMP;
        MaSoThue = entity.MaSoThue;
        HanThanhToan = entity.HanThanhToan;
        TyLeVAT = entity.TyLeVAT;
        LoaiNhapXuat = entity.LoaiNhapXuat;
        Toa = entity.Toa;
        IdId = entity.IdId;
        LPhieu = entity.LPhieu;
        SHTK = entity.SHTK;
        MaHangHoa = entity.MaHangHoa;
        TenHangHoa = entity.TenHangHoa;
        SoLuongTon = entity.SoLuongTon;
        SoLuong = entity.SoLuong;
        DonGia = entity.DonGia;
        DonViTinh = entity.DonViTinh;
        SoTien = entity.SoTien;
        SoTienUSD = entity.SoTienUSD;
        DonGiaUSD = entity.DonGiaUSD;
        MaDonVi = entity.MaDonVi;
        TenDonVi = entity.TenDonVi;
        MaDonVi1 = entity.MaDonVi1;
        TenDonVi1 = entity.TenDonVi1;
        HanSuDung = entity.HanSuDung;
        KetChuyen1 = entity.KetChuyen1;
        GiaVon = entity.GiaVon;
        GiaVonUSD = entity.GiaVonUSD;
        MaPhanBo = entity.MaPhanBo;
        TenPhanBo = entity.TenPhanBo;
        Comment = entity.Comment;
        CapNhatGiaVon = entity.CapNhatGiaVon;
        ThanhToan = entity.ThanhToan;
        SoTienTTVND = entity.SoTienTTVND;
        SoTienTTUSD = entity.SoTienTTUSD;
        MaDonViSuDung = entity.MaDonViSuDung;
        TenDonViSuDung = entity.TenDonViSuDung;
        TyLeChietKhau = entity.TyLeChietKhau;
        SoTienChietKhau = entity.SoTienChietKhau;
        SoTienXuat = entity.SoTienXuat;
        DonGiaXuat = entity.DonGiaXuat;
        PhiVanChuyen = entity.PhiVanChuyen;
        ChenhLech = entity.ChenhLech;
        XuatXu = entity.XuatXu;
        QuyCach = entity.QuyCach;
        SoPhieuYeuCau = entity.SoPhieuYeuCau;
        ChonIn = entity.ChonIn;
        DuAn = entity.DuAn;
        HinhThucTT = entity.HinhThucTT;
        NgayHopDong = entity.NgayHopDong;
        SoHopDong = entity.SoHopDong;
        NoiGiaoHang = entity.NoiGiaoHang;
        ThoiGianGiaoHang = entity.ThoiGianGiaoHang;
        TeamOK = entity.TeamOK;
        SoTienOK = entity.SoTienOK;
        TyLeCK = entity.TyLeCK;
        SoTienTT = entity.SoTienTT;
        ChietKhau = entity.ChietKhau;
        TenKho1 = entity.TenKho1;
        MAKHO1 = entity.MAKHO1;
        TenNguoiThan = entity.TenNguoiThan;
        MaNguoiThan = entity.MaNguoiThan;
        MaNguoiChuyen = entity.MaNguoiChuyen;
        TenNguoiChuyen = entity.TenNguoiChuyen;
        SoTienBanLe = entity.SoTienBanLe;
        DonGiaBanLe = entity.DonGiaBanLe;
        MaBarCode = entity.MaBarCode;
        SoTienVAT = entity.SoTienVAT;
        TyLeVATOK = entity.TyLeVATOK;
        SoTienVATOK = entity.SoTienVATOK;
        ChieuDai = entity.ChieuDai;
        KhoRongTon = entity.KhoRongTon;
        TongDienTich = entity.TongDienTich;
        TongChieuDai = entity.TongChieuDai;
        LoaiTon = entity.LoaiTon;
        MauSac = entity.MauSac;
        DoDay = entity.DoDay;
        KieuSong = entity.KieuSong;
        ChungLoai = entity.ChungLoai;
        MaLoaiTon = entity.MaLoaiTon;
        MaMauSac = entity.MaMauSac;
        MaDoDay = entity.MaDoDay;
        MaKieuSong = entity.MaKieuSong;
        MaChungLoai = entity.MaChungLoai;
        DiaDiem = entity.DiaDiem;
        ThoiGianGiao = entity.ThoiGianGiao;
        NguoiQL = entity.NguoiQL;
        NgayGiao = entity.NgayGiao;
        MaQuanLy = entity.MaQuanLy;
        TenQuanLy = entity.TenQuanLy;
        TyLeCkNv = entity.TyLeCkNv;
        SoTienCkNv = entity.SoTienCkNv;
        ImgQrCode = entity.ImgQrCode;
        SoPhieuXuat = entity.SoPhieuXuat;
        SoPhieuLSX = entity.SoPhieuLSX;
        LoHang = entity.LoHang;
        TrangThaiDetail = entity.TrangThaiDetail;
        NgayXacNhan = entity.NgayXacNhan;
        NgayHuy = entity.NgayHuy;
        TienHang = entity.TienHang;
        HinhThucGiaoHang = entity.HinhThucGiaoHang;
        TrangThai = entity.TrangThai;
        MaTinh = entity.MaTinh;
        TenTinh = entity.TenTinh;
        CapDL = entity.CapDL;
        SoTienCK = entity.SoTienCK;
        DiaChiQuanLy = entity.DiaChiQuanLy;
        SoTienGiam = entity.SoTienGiam;
        MaNhom = entity.MaNhom;
        NgayPhieuLSX = entity.NgayPhieuLSX;
        CreatedDate = entity.CreatedDate;
        NgayXuatKho = entity.NgayXuatKho;
    }

    public PhieuNhapXuatAllModel UpdateLastVersion(PhieuNhapXuatAllModel model)
    {
        Id = model.Id;
        Loai = model.Loai;
        LoaiPhieu = model.LoaiPhieu;
        Stt = model.Stt;
        Phieu = model.Phieu;
        NgayCT = model.NgayCT;
        NgayGhi = model.NgayGhi;
        SoChungTu = model.SoChungTu;
        DienGiai = model.DienGiai;
        TongCong = model.TongCong;
        MaDoiTuong = model.MaDoiTuong;
        TenDoiTuong = model.TenDoiTuong;
        DiaChiDoiTuong = model.DiaChiDoiTuong;
        KemTheo = model.KemTheo;
        Selected = model.Selected;
        UserName = model.UserName;
        GhiChu = model.GhiChu;
        MaKho = model.MaKho;
        TenKho = model.TenKho;
        MaLyDo = model.MaLyDo;
        TenLyDo = model.TenLyDo;
        LoaiTien = model.LoaiTien;
        TyGia = model.TyGia;
        SoHoaDon = model.SoHoaDon;
        TyLeVATTMP = model.TyLeVATTMP;
        MaSoThue = model.MaSoThue;
        HanThanhToan = model.HanThanhToan;
        TyLeVAT = model.TyLeVAT;
        LoaiNhapXuat = model.LoaiNhapXuat;
        Toa = model.Toa;
        IdId = model.IdId;
        LPhieu = model.LPhieu;
        SHTK = model.SHTK;
        MaHangHoa = model.MaHangHoa;
        TenHangHoa = model.TenHangHoa;
        SoLuongTon = model.SoLuongTon;
        SoLuong = model.SoLuong;
        DonGia = model.DonGia;
        DonViTinh = model.DonViTinh;
        SoTien = model.SoTien;
        SoTienUSD = model.SoTienUSD;
        DonGiaUSD = model.DonGiaUSD;
        MaDonVi = model.MaDonVi;
        TenDonVi = model.TenDonVi;
        MaDonVi1 = model.MaDonVi1;
        TenDonVi1 = model.TenDonVi1;
        HanSuDung = model.HanSuDung;
        KetChuyen1 = model.KetChuyen1;
        GiaVon = model.GiaVon;
        GiaVonUSD = model.GiaVonUSD;
        MaPhanBo = model.MaPhanBo;
        TenPhanBo = model.TenPhanBo;
        Comment = model.Comment;
        CapNhatGiaVon = model.CapNhatGiaVon;
        ThanhToan = model.ThanhToan;
        SoTienTTVND = model.SoTienTTVND;
        SoTienTTUSD = model.SoTienTTUSD;
        MaDonViSuDung = model.MaDonViSuDung;
        TenDonViSuDung = model.TenDonViSuDung;
        TyLeChietKhau = model.TyLeChietKhau;
        SoTienChietKhau = model.SoTienChietKhau;
        SoTienXuat = model.SoTienXuat;
        DonGiaXuat = model.DonGiaXuat;
        PhiVanChuyen = model.PhiVanChuyen;
        ChenhLech = model.ChenhLech;
        XuatXu = model.XuatXu;
        QuyCach = model.QuyCach;
        SoPhieuYeuCau = model.SoPhieuYeuCau;
        ChonIn = model.ChonIn;
        DuAn = model.DuAn;
        HinhThucTT = model.HinhThucTT;
        NgayHopDong = model.NgayHopDong;
        SoHopDong = model.SoHopDong;
        NoiGiaoHang = model.NoiGiaoHang;
        ThoiGianGiaoHang = model.ThoiGianGiaoHang;
        TeamOK = model.TeamOK;
        SoTienOK = model.SoTienOK;
        TyLeCK = model.TyLeCK;
        SoTienTT = model.SoTienTT;
        ChietKhau = model.ChietKhau;
        TenKho1 = model.TenKho1;
        MAKHO1 = model.MAKHO1;
        TenNguoiThan = model.TenNguoiThan;
        MaNguoiThan = model.MaNguoiThan;
        MaNguoiChuyen = model.MaNguoiChuyen;
        TenNguoiChuyen = model.TenNguoiChuyen;
        SoTienBanLe = model.SoTienBanLe;
        DonGiaBanLe = model.DonGiaBanLe;
        MaBarCode = model.MaBarCode;
        SoTienVAT = model.SoTienVAT;
        TyLeVATOK = model.TyLeVATOK;
        SoTienVATOK = model.SoTienVATOK;
        ChieuDai = model.ChieuDai;
        KhoRongTon = model.KhoRongTon;
        TongDienTich = model.TongDienTich;
        TongChieuDai = model.TongChieuDai;
        LoaiTon = model.LoaiTon;
        MauSac = model.MauSac;
        DoDay = model.DoDay;
        KieuSong = model.KieuSong;
        ChungLoai = model.ChungLoai;
        MaLoaiTon = model.MaLoaiTon;
        MaMauSac = model.MaMauSac;
        MaDoDay = model.MaDoDay;
        MaKieuSong = model.MaKieuSong;
        MaChungLoai = model.MaChungLoai;
        DiaDiem = model.DiaDiem;
        ThoiGianGiao = model.ThoiGianGiao;
        NguoiQL = model.NguoiQL;
        NgayGiao = model.NgayGiao;
        MaQuanLy = model.MaQuanLy;
        TenQuanLy = model.TenQuanLy;
        TyLeCkNv = model.TyLeCkNv;
        SoTienCkNv = model.SoTienCkNv;
        ImgQrCode = model.ImgQrCode;
        SoPhieuXuat = model.SoPhieuXuat;
        SoPhieuLSX = model.SoPhieuLSX;
        LoHang = model.LoHang;
        TrangThaiDetail = model.TrangThaiDetail;
        NgayXacNhan = model.NgayXacNhan;
        NgayHuy = model.NgayHuy;
        TienHang = model.TienHang;
        HinhThucGiaoHang = model.HinhThucGiaoHang;
        TrangThai = model.TrangThai;
        MaTinh = model.MaTinh;
        TenTinh = model.TenTinh;
        CapDL = model.CapDL;
        SoTienCK = model.SoTienCK;
        DiaChiQuanLy = model.DiaChiQuanLy;
        SoTienGiam = model.SoTienGiam;
        MaNhom = model.MaNhom;
        NgayPhieuLSX = model.NgayPhieuLSX;
        CreatedDate = model.CreatedDate;
        NgayXuatKho = model.NgayXuatKho;
        return this;
    }

    public string TrangThaiChiTietDisplay
    {
        get
        {
            if (TrangThaiDetail == null)
            {
                return string.Empty;
            }
            switch (TrangThaiDetail)
            {
                case 1:
                    return "Lên đơn";
                case 2:
                    return "Đang sản xuất";
                case 3:
                    return "Đang giao hàng";
                case 4:
                    return "Giao hàng xong";
                case 5:
                    return "Hủy đơn hàng";
                default:
                    return string.Empty;
            }
        }
    }

    public string TrangThaiDonHangDisplay
    {
        get
        {
            if (TrangThai == null)
            {
                return string.Empty;
            }
            switch (TrangThai)
            {
                case 1:
                    return "Lên đơn";
                case 2:
                    return "Đang sản xuất";
                case 3:
                    return "Đang giao hàng";
                case 4:
                    return "Giao hàng xong";
                case 5:
                    return "Hủy đơn hàng";
                default:
                    return string.Empty;
            }
        }
    }

    // Id
    public int Id { get; set; }

    // LOAI
    public string? Loai { get; set; }

    // LOAIPHIEU
    public string? LoaiPhieu { get; set; }

    // STT
    public string? Stt { get; set; }

    // PHIEU
    public string? Phieu { get; set; }

    // NGAYCT
    public DateTime? NgayCT { get; set; }

    // NGAYGHI
    public DateTime? NgayGhi { get; set; }

    // SOCHUNGTU
    public string? SoChungTu { get; set; }

    // DIENGIAI
    public string? DienGiai { get; set; }

    // TONGCONG
    public double? TongCong { get; set; }

    // MADOITUONG
    public string? MaDoiTuong { get; set; }

    // TENDOITUONG
    public string? TenDoiTuong { get; set; }

    // DIACHIDOITUONG
    public string? DiaChiDoiTuong { get; set; }

    // KEMTHEO
    public string? KemTheo { get; set; }

    // SELECTTED
    public bool? Selected { get; set; }

    // USERNAME
    public string? UserName { get; set; }

    // GHICHU
    public string? GhiChu { get; set; }

    // MAKHO
    public string? MaKho { get; set; }

    // TENKHO
    public string? TenKho { get; set; }

    // MALYDO
    public string? MaLyDo { get; set; }

    // TENLYDO
    public string? TenLyDo { get; set; }

    // LOAITIEN
    public string? LoaiTien { get; set; }

    // TYGIA
    public double? TyGia { get; set; }

    // SOHOADON
    public string? SoHoaDon { get; set; }

    // TYLEVATTMP
    public string? TyLeVATTMP { get; set; }

    // MASOTHUE
    public string? MaSoThue { get; set; }

    // HANTHANHTOAN
    public DateTime? HanThanhToan { get; set; }

    // TYLEVAT
    public double? TyLeVAT { get; set; }

    // LOAINHAPXUAT
    public string? LoaiNhapXuat { get; set; }

    // TOA
    public string? Toa { get; set; }

    // IDID
    public int IdId { get; set; }

    // LPHIEU
    public string? LPhieu { get; set; }

    // SHTK
    public string? SHTK { get; set; }

    // MAHANGHOA
    public string? MaHangHoa { get; set; }

    // TENHANGHOA
    public string? TenHangHoa { get; set; }

    // SOLUONGTON
    public double? SoLuongTon { get; set; }

    // SOLUONG
    public double? SoLuong { get; set; }

    // DONGIA
    public double? DonGia { get; set; }

    // DONVITINH
    public string? DonViTinh { get; set; }

    // SOTIEN
    public double? SoTien { get; set; }

    // SOTIENUSD
    public double? SoTienUSD { get; set; }

    // DONGIAUSD
    public double? DonGiaUSD { get; set; }

    // MADONVI
    public string? MaDonVi { get; set; }

    // TENDONVI
    public string? TenDonVi { get; set; }

    // MADONVI1
    public string? MaDonVi1 { get; set; }

    // TENDONVI1
    public string? TenDonVi1 { get; set; }

    // HANSUDUNG
    public DateTime? HanSuDung { get; set; }

    // KETCHUYEN1
    public string? KetChuyen1 { get; set; }

    // GIAVON
    public double? GiaVon { get; set; }

    // GIAVONUSD
    public double? GiaVonUSD { get; set; }

    // MAPHANBO
    public string? MaPhanBo { get; set; }

    // TENPHANBO
    public string? TenPhanBo { get; set; }

    // COMMENT
    public string? Comment { get; set; }

    // CAPNHATGIAVON
    public bool? CapNhatGiaVon { get; set; }

    // THANHTOAN
    public bool? ThanhToan { get; set; }

    // SOTIENTTVND
    public double? SoTienTTVND { get; set; }

    // SOTIENTTUSD
    public double? SoTienTTUSD { get; set; }

    // MADONVISUDUNG
    public string? MaDonViSuDung { get; set; }

    // TENDONVISUDUNG
    public string? TenDonViSuDung { get; set; }

    // TYLECHIETKHAU
    public double? TyLeChietKhau { get; set; }

    // SOTIENCHIETKHAU
    public double? SoTienChietKhau { get; set; }

    // SOTIENXUAT
    public double? SoTienXuat { get; set; }

    // DONGIAXUAT
    public double? DonGiaXuat { get; set; }

    // PHIVANCHUYEN
    public double? PhiVanChuyen { get; set; }

    // CHENHLECH
    public double? ChenhLech { get; set; }

    // XUATXU
    public string? XuatXu { get; set; }

    // QUYCACH
    public string? QuyCach { get; set; }

    // SOPHIEUYEUCAU
    public string? SoPhieuYeuCau { get; set; }

    // CHONIN
    public bool? ChonIn { get; set; }

    // DUAN
    public bool? DuAn { get; set; }

    // HINHTHUCTT
    public string? HinhThucTT { get; set; }

    // NGAYHOPDONG
    public DateTime? NgayHopDong { get; set; }

    // SOHOPDONG
    public string? SoHopDong { get; set; }

    // NOIGIAOHANG
    public string? NoiGiaoHang { get; set; }

    // THOIGIANGIAOHANG
    public string? ThoiGianGiaoHang { get; set; }

    // TEAMOK
    public string? TeamOK { get; set; }

    // SOTIENOK
    public double? SoTienOK { get; set; }

    // TYLECK
    public double? TyLeCK { get; set; }

    // SOTIENTT
    public double? SoTienTT { get; set; }

    // CHIETKHAU
    public bool? ChietKhau { get; set; }

    // TENKHO1
    public string? TenKho1 { get; set; }

    // MaKho1
    public string? MAKHO1 { get; set; }

    // TENNGUOINHAN
    public string? TenNguoiThan { get; set; }

    // MANGUOINHAN
    public string? MaNguoiThan { get; set; }

    // MANGUOICHUYEN
    public string? MaNguoiChuyen { get; set; }

    // TENNGUOICHUYEN
    public string? TenNguoiChuyen { get; set; }

    // SOTIENBANLE
    public double? SoTienBanLe { get; set; }

    // DONGIABANLE
    public double? DonGiaBanLe { get; set; }

    // MABARCODE
    public string? MaBarCode { get; set; }

    // SOTIENVAT
    public double? SoTienVAT { get; set; }

    // TYLEVATOK
    public double? TyLeVATOK { get; set; }

    // SOTIENVATOK
    public double? SoTienVATOK { get; set; }

    // CHIEUDAI
    public double? ChieuDai { get; set; }

    // KHORONGTON
    public double? KhoRongTon { get; set; }

    // TONGDIENTICH
    public double? TongDienTich { get; set; }

    // TONGCHIEUDAI
    public double? TongChieuDai { get; set; }

    // LOAITON
    public string? LoaiTon { get; set; }

    // MAUSAC
    public string? MauSac { get; set; }

    // DODAY
    public string? DoDay { get; set; }

    // KIEUSONG
    public string? KieuSong { get; set; }

    // CHUNGLOAI
    public string? ChungLoai { get; set; }

    // MALOAITON
    public string? MaLoaiTon { get; set; }

    // MAMAUSAC
    public string? MaMauSac { get; set; }

    // MADODAY
    public string? MaDoDay { get; set; }

    // MAKIEUSONG
    public string? MaKieuSong { get; set; }

    // MACHUNGLOAI
    public string? MaChungLoai { get; set; }

    // DIADIEM
    public string? DiaDiem { get; set; }

    // THOIGIANGIAO
    public string? ThoiGianGiao { get; set; }

    // NGUOIQL
    public string? NguoiQL { get; set; }

    // NGAYGIAO
    public DateTime? NgayGiao { get; set; }

    // MAQUANLY
    public string? MaQuanLy { get; set; }

    // TENQUANLY
    public string? TenQuanLy { get; set; }

    // TYLE_CK_NV
    public double? TyLeCkNv { get; set; }

    // SOTIEN_CK_NV
    public double? SoTienCkNv { get; set; }

    // IMG_QRCODE
    public byte? ImgQrCode { get; set; }

    // SOPHIEUXUAT
    public string? SoPhieuXuat { get; set; }

    // SOPHIEULSX
    public string? SoPhieuLSX { get; set; }

    // LOHANG
    public string? LoHang { get; set; }

    // TRANGTHAI_DETAIL
    public int? TrangThaiDetail { get; set; }

    // NGAYXACNHAN
    public DateTime? NgayXacNhan { get; set; }

    // NGAYHUY
    public DateTime? NgayHuy { get; set; }

    // TIENHANG
    public double? TienHang { get; set; }

    // HINHTHUCGIAOHANG
    public string? HinhThucGiaoHang { get; set; }

    // TRANGTHAI
    public int? TrangThai { get; set; }

    // MATINH
    public string? MaTinh { get; set; }

    // TENTINH
    public string? TenTinh { get; set; }

    // CAPDL
    public int? CapDL { get; set; }

    // SOTIENCK
    public double? SoTienCK { get; set; }

    // DIACHIQUANLY
    public string? DiaChiQuanLy { get; set; }

    // SOTIEN_GIAM
    public double? SoTienGiam { get; set; }

    // MANHOM
    public string? MaNhom { get; set; }

    // NGAYPHIEULSX
    public DateTime? NgayPhieuLSX { get; set; }

    // NGAYXUATKHO
    public DateTime? NgayXuatKho { get; set; }

    public DateTime CreatedDate { get; set; }

    // NGAYCTDisplay
    public string NgayCTDisplay
    {
        get
        {
            return NgayCT?.ToString("dd/MM/yyyy") ?? string.Empty;
        }
    }

    // NgayPhieuLSXDisplay
    public string NgayPhieuLSXDisplay
    {
        get
        {
            return NgayPhieuLSX?.ToString("dd/MM/yyyy HH:mm:ss") ?? string.Empty;
        }
    }

    // NgayXuatKhoDisplay
    public string NgayXuatKhoDisplay
    {
        get
        {
            return NgayXuatKho?.ToString("dd/MM/yyyy HH:mm:ss") ?? string.Empty;
        }
    }
}
