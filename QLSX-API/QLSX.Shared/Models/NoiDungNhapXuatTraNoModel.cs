using QLSX.Shared.Entities;
using System;

namespace QLSX.Shared.Models;

public class NoiDungNhapXuatTraNoModel
{
    public NoiDungNhapXuatTraNoModel()
    {
    }

    public NoiDungNhapXuatTraNoModel(NoiDungNhapXuatTraNo entity)
    {
        Id = entity.Id;
        IdId = entity.IdId;
        LoaiPhieu = entity.LoaiPhieu;
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
        TyLeChietKhau = entity.TyLeChietKhau;
        SoTienChietKhau = entity.SoTienChietKhau;
        SoTienXuat = entity.SoTienXuat;
        DonGiaXuat = entity.DonGiaXuat;
        ChenhLech = entity.ChenhLech;
        XuatXu = entity.XuatXu;
        QuyCach = entity.QuyCach;
        MaNhom = entity.MaNhom;
        MaLoai = entity.MaLoai;
        TenNhom = entity.TenNhom;
        TenLoai = entity.TenLoai;
        PhuongThuc = entity.PhuongThuc;
        DienGiai = entity.DienGiai;
        TongChieuDai = entity.TongChieuDai;
        TongDienTich = entity.TongDienTich;
        KhoRongTon = entity.KhoRongTon;
        ChieuDai = entity.ChieuDai;
        MaHangHoa1 = entity.MaHangHoa1;
        TenHangHoa1 = entity.TenHangHoa1;
        DonViTinh1 = entity.DonViTinh1;
        SoLuong1 = entity.SoLuong1;
        ThueNK = entity.ThueNK;
        ThueVAT = entity.ThueVAT;
        TyLePhiVanChuyen = entity.TyLePhiVanChuyen;
        SoTienPhiVanChuyen = entity.SoTienPhiVanChuyen;
        TyLeKhuyenMai = entity.TyLeKhuyenMai;
        SoTienKhuyenMai = entity.SoTienKhuyenMai;
        TongCong = entity.TongCong;
        TinhChat = entity.TinhChat;
        Kieu = entity.Kieu;
        SoTienVND = entity.SoTienVND;
        POLY = entity.POLY;
        MDI = entity.MDI;
        MaHoaChatPoly = entity.MaHoaChatPoly;
        MaHoaChatMDI = entity.MaHoaChatMDI;
        MaDonViNhan = entity.MaDonViNhan;
        TenDonViNhan = entity.TenDonViNhan;
        NuocSanXuat = entity.NuocSanXuat;
        TyLeCK = entity.TyLeCK;
        SoTienOk = entity.SoTienOK;
        DonGiaBan = entity.DonGiaBan;
        SoTienBan = entity.SoTienBan;
        MaKho = entity.MaKho;
        ThoiGianTao = entity.ThoiGianTao;
        SoLuongTra = entity.SoLuongTra;
        LoHang = entity.LoHang;
        MaBarCode = entity.MaBarCode;
        DonGiaBanLe = entity.DonGiaBanLe;
        SoTienBanLe = entity.SoTienBanLe;
        TyLeVAT = entity.TyLeVAT;
        SoTienVAT = entity.SoTienVAT;
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
        TyTrong = entity.TyTrong;
        DonGiaDVT1 = entity.DonGiaDVT1;
        SoLuongDVT1 = entity.SoLuongDVT1;
        SoTienDVT1 = entity.SoTienDVT1;
        TyLeCkNV = entity.TyLeCkNV;
        SoTienCkNv = entity.SoTienCkNv;
        SoPhieuLSX = entity.SoPhieuLSX;
        NgayPhieuLSX = entity.NgayPhieuLSX;
        TrangThaiDetail = entity.TrangThaiDetail;
        SoPhieuXuat = entity.SoPhieuXuat;
        NgayXuatKho = entity.NgayXuatKho;
        NgayXacNhan = entity.NgayXacNhan;
        NhapXuatId = entity.NhapXuatId;
    }

    public NoiDungNhapXuatTraNoModel(NoiDungNhapXuatModel noiDungNhapXuatModel)
    {
        Stt = noiDungNhapXuatModel.Stt;
        Id = noiDungNhapXuatModel.Id;
        IdId = noiDungNhapXuatModel.IdId;
        LoaiPhieu = noiDungNhapXuatModel.LoaiPhieu;
        SHTK = noiDungNhapXuatModel.SHTK;
        MaHangHoa = noiDungNhapXuatModel.MaHangHoa;
        TenHangHoa = noiDungNhapXuatModel.TenHangHoa;
        SoLuongTon = noiDungNhapXuatModel.SoLuongTon;
        SoLuong = noiDungNhapXuatModel.SoLuong;
        DonGia = noiDungNhapXuatModel.DonGia;
        DonViTinh = noiDungNhapXuatModel.DonViTinh;
        SoTien = noiDungNhapXuatModel.SoTien;
        SoTienUSD = noiDungNhapXuatModel.SoTienUSD;
        DonGiaUSD = noiDungNhapXuatModel.DonGiaUSD;
        MaDonVi = noiDungNhapXuatModel.MaDonVi;
        TenDonVi = noiDungNhapXuatModel.TenDonVi;
        MaDonVi1 = noiDungNhapXuatModel.MaDonVi1;
        TenDonVi1 = noiDungNhapXuatModel.TenDonVi1;
        HanSuDung = noiDungNhapXuatModel.HanSuDung;
        KetChuyen1 = noiDungNhapXuatModel.KetChuyen1;
        GiaVon = noiDungNhapXuatModel.GiaVon;
        GiaVonUSD = noiDungNhapXuatModel.GiaVonUSD;
        MaPhanBo = noiDungNhapXuatModel.MaPhanBo;
        TenPhanBo = noiDungNhapXuatModel.TenPhanBo;
        Comment = noiDungNhapXuatModel.Comment;
        CapNhatGiaVon = noiDungNhapXuatModel.CapNhatGiaVon;
        TyLeChietKhau = noiDungNhapXuatModel.TyLeChietKhau;
        SoTienChietKhau = noiDungNhapXuatModel.SoTienChietKhau;
        SoTienXuat = noiDungNhapXuatModel.SoTienXuat;
        DonGiaXuat = noiDungNhapXuatModel.DonGiaXuat;
        ChenhLech = noiDungNhapXuatModel.ChenhLech;
        XuatXu = noiDungNhapXuatModel.XuatXu;
        QuyCach = noiDungNhapXuatModel.QuyCach;
        MaNhom = noiDungNhapXuatModel.MaNhom;
        MaLoai = noiDungNhapXuatModel.MaLoai;
        TenNhom = noiDungNhapXuatModel.TenNhom;
        TenLoai = noiDungNhapXuatModel.TenLoai;
        PhuongThuc = noiDungNhapXuatModel.PhuongThuc;
        DienGiai = noiDungNhapXuatModel.GhiChu;
        TongChieuDai = noiDungNhapXuatModel.TongChieuDai;
        TongDienTich = noiDungNhapXuatModel.TongDienTich;
        KhoRongTon = noiDungNhapXuatModel.KhoRongTon;
        ChieuDai = noiDungNhapXuatModel.ChieuDai;
        MaHangHoa1 = noiDungNhapXuatModel.MaHangHoa1;
        TenHangHoa1 = noiDungNhapXuatModel.TenHangHoa1;
        DonViTinh1 = noiDungNhapXuatModel.DonViTinh1;
        SoLuong1 = noiDungNhapXuatModel.SoLuong1;
        ThueNK = noiDungNhapXuatModel.ThueNK;
        ThueVAT = noiDungNhapXuatModel.ThueVAT;
        TyLePhiVanChuyen = noiDungNhapXuatModel.TyLePhiVanChuyen;
        SoTienPhiVanChuyen = noiDungNhapXuatModel.SoTienPhiVanChuyen;
        TyLeKhuyenMai = noiDungNhapXuatModel.TyLeKhuyenMai;
        SoTienKhuyenMai = noiDungNhapXuatModel.SoTienKhuyenMai;
        TongCong = noiDungNhapXuatModel.TongCong;
        TinhChat = noiDungNhapXuatModel.TinhChat;
        Kieu = noiDungNhapXuatModel.Kieu;
        SoTienVND = noiDungNhapXuatModel.SoTienVND;
        POLY = noiDungNhapXuatModel.POLY;
        MDI = noiDungNhapXuatModel.MDI;
        MaHoaChatPoly = noiDungNhapXuatModel.MaHoaChatPoly;
        MaHoaChatMDI = noiDungNhapXuatModel.MaHoaChatMDI;
        MaDonViNhan = noiDungNhapXuatModel.MaDonViNhan;
        TenDonViNhan = noiDungNhapXuatModel.TenDonViNhan;
        NuocSanXuat = noiDungNhapXuatModel.NuocSanXuat;
        TyLeCK = noiDungNhapXuatModel.TyLeCK ?? 0;
        SoTienOk = noiDungNhapXuatModel.SoTienOk;
        DonGiaBan = noiDungNhapXuatModel.DonGiaBan;
        SoTienBan = noiDungNhapXuatModel.SoTienBan;
        MaKho = noiDungNhapXuatModel.MaKho;
        ThoiGianTao = noiDungNhapXuatModel.ThoiGianTao;
        SoLuongTra = noiDungNhapXuatModel.SoLuongTra;
        LoHang = noiDungNhapXuatModel.LoHang;
        MaBarCode = noiDungNhapXuatModel.MaBarCode;
        DonGiaBanLe = noiDungNhapXuatModel.DonGiaBanLe;
        SoTienBanLe = noiDungNhapXuatModel.SoTienBanLe;
        TyLeVAT = noiDungNhapXuatModel.TyLeVAT;
        SoTienVAT = noiDungNhapXuatModel.SoTienVAT;
        LoaiTon = noiDungNhapXuatModel.LoaiTon;
        MauSac = noiDungNhapXuatModel.MauSac;
        DoDay = noiDungNhapXuatModel.DoDay;
        KieuSong = noiDungNhapXuatModel.KieuSong;
        ChungLoai = noiDungNhapXuatModel.ChungLoai;
        MaLoaiTon = noiDungNhapXuatModel.MaLoaiTon;
        MaMauSac = noiDungNhapXuatModel.MaMauSac;
        MaDoDay = noiDungNhapXuatModel.MaDoDay;
        MaKieuSong = noiDungNhapXuatModel.MaKieuSong;
        MaChungLoai = noiDungNhapXuatModel.MaChungLoai;
        TyTrong = noiDungNhapXuatModel.TyTrong ?? 0;
        DonGiaDVT1 = noiDungNhapXuatModel.DonGiaDVT1 ?? 0;
        SoLuongDVT1 = noiDungNhapXuatModel.SoLuongDVT1 ?? 0;
        SoTienDVT1 = noiDungNhapXuatModel.SoTienDVT1 ?? 0;
        TyLeCkNV = noiDungNhapXuatModel.TyLeCkNv ?? 0;
        SoTienCkNv = noiDungNhapXuatModel.SoTienCkNv ?? 0;
        SoPhieuLSX = noiDungNhapXuatModel.SoPhieuLSX;
        NgayPhieuLSX = noiDungNhapXuatModel.NgayPhieuLSX;
        TrangThaiDetail = noiDungNhapXuatModel.TrangThaiDetail;
        SoPhieuXuat = noiDungNhapXuatModel.SoPhieuXuat;
        NgayXuatKho = noiDungNhapXuatModel.NgayXuatKho;
        NgayXacNhan = noiDungNhapXuatModel.NgayXacNhan;
        NhapXuatId = noiDungNhapXuatModel.NhapXuatId;
    }


    public string Stt { get; set; }

    public int Id { get; set; }

    public int? IdId { get; set; }

    public string? LoaiPhieu { get; set; }

    public string? SHTK { get; set; }

    public string? MaHangHoa { get; set; }

    public string? TenHangHoa { get; set; }

    public double? SoLuongTon { get; set; }

    public double? SoLuong { get; set; }

    public double? DonGia { get; set; }

    public string? DonViTinh { get; set; }

    public double? SoTien { get; set; }

    public double? SoTienUSD { get; set; }

    public double? DonGiaUSD { get; set; }

    public string? MaDonVi { get; set; }

    public string? TenDonVi { get; set; }

    public string? MaDonVi1 { get; set; }

    public string? TenDonVi1 { get; set; }

    public DateTime? HanSuDung { get; set; }

    public string? KetChuyen1 { get; set; }

    public double? GiaVon { get; set; }

    public double? GiaVonUSD { get; set; }

    public string? MaPhanBo { get; set; }

    public string? TenPhanBo { get; set; }

    public string? Comment { get; set; }

    public bool? CapNhatGiaVon { get; set; }

    public double? TyLeChietKhau { get; set; }

    public double? SoTienChietKhau { get; set; }

    public double? SoTienXuat { get; set; }

    public double? DonGiaXuat { get; set; }

    public double? ChenhLech { get; set; }

    public string? XuatXu { get; set; }

    public string? QuyCach { get; set; }

    public string? MaNhom { get; set; }

    public string? MaLoai { get; set; }

    public string? TenNhom { get; set; }

    public string? TenLoai { get; set; }

    public string? PhuongThuc { get; set; }

    public string? DienGiai { get; set; }

    public double? TongChieuDai { get; set; }

    public double? TongDienTich { get; set; }

    public double? KhoRongTon { get; set; }

    public double? ChieuDai { get; set; }

    public string? MaHangHoa1 { get; set; }

    public string? TenHangHoa1 { get; set; }

    public string? DonViTinh1 { get; set; }

    public double? SoLuong1 { get; set; }

    public double? ThueNK { get; set; }

    public double? ThueVAT { get; set; }

    public double? TyLePhiVanChuyen { get; set; }

    public double? SoTienPhiVanChuyen { get; set; }

    public double? TyLeKhuyenMai { get; set; }

    public double? SoTienKhuyenMai { get; set; }

    public double? TongCong { get; set; }

    public string? TinhChat { get; set; }

    public string? Kieu { get; set; }

    public double? SoTienVND { get; set; }

    public double? POLY { get; set; }

    public double? MDI { get; set; }

    public string? MaHoaChatPoly { get; set; }

    public string? MaHoaChatMDI { get; set; }

    public string? MaDonViNhan { get; set; }

    public string? TenDonViNhan { get; set; }

    public string? NuocSanXuat { get; set; }

    public double? TyLeCK { get; set; }

    public double? SoTienOk { get; set; }

    public double? DonGiaBan { get; set; }

    public double? SoTienBan { get; set; }

    public string? MaKho { get; set; }

    public string? ThoiGianTao { get; set; }

    public double? SoLuongTra { get; set; }

    public string? LoHang { get; set; }

    public string? MaBarCode { get; set; }

    public double? DonGiaBanLe { get; set; }

    public double? SoTienBanLe { get; set; }

    public double? TyLeVAT { get; set; }

    public double? SoTienVAT { get; set; }

    public string? LoaiTon { get; set; }

    public string? MauSac { get; set; }

    public string? DoDay { get; set; }

    public string? KieuSong { get; set; }

    public string? ChungLoai { get; set; }

    public string? MaLoaiTon { get; set; }

    public string? MaMauSac { get; set; }

    public string? MaDoDay { get; set; }

    public string? MaKieuSong { get; set; }

    public string? MaChungLoai { get; set; }

    public double? TyTrong { get; set; }

    public double? DonGiaDVT1 { get; set; }

    public double? SoLuongDVT1 { get; set; }

    public double? SoTienDVT1 { get; set; }

    public double? TyLeCkNV { get; set; }

    public double? SoTienCkNv { get; set; }

    public string? SoPhieuLSX { get; set; }

    public DateTime? NgayPhieuLSX { get; set; }

    public int? TrangThaiDetail { get; set; }

    public string? SoPhieuXuat { get; set; }

    public DateTime? NgayXuatKho { get; set; }

    public DateTime? NgayXacNhan { get; set; }

    public int? NhapXuatId { get; set; }

    public NoiDungNhapXuatTraNoModel CalculateSoTien()
    {
        // Tổng chiều dài
        TongChieuDai = ChieuDai == null || ChieuDai == 0 ? null : ChieuDai * SoLuong;

        // Tổng diện tích
        TongDienTich = KhoRongTon != null && TongChieuDai != null && TongChieuDai != 0 ? TongChieuDai * KhoRongTon : null;

        // Số tiền
        SoTien = TongDienTich != null ? TongDienTich * DonGia : (TongChieuDai != null ? TongChieuDai * DonGia : (SoLuong * DonGia));
        SoTienOk = SoTien;
        return this;
    }
}
