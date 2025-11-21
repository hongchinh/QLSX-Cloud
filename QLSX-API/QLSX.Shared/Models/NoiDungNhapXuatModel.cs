using QLSX.Shared.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Models;

public class NoiDungNhapXuatModel : BaseModel
{
    public NoiDungNhapXuatModel()
    {
    }

    public NoiDungNhapXuatModel(NoiDungNhapXuat entity)
    {
        if (entity != null)
        {
            Id = entity.IdId;
            MaHangHoa = entity.MaHangHoa;
            TenHangHoa = entity.TenHangHoa;
            DonViTinh = entity.DonViTinh;
            SoLuong = entity.SoLuong ?? 0;
            DonGia = entity.DonGia ?? 0;
            SoTien = entity.SoTien ?? 0;
            DonGiaHoaHong = entity.TyLeCkNv ?? 0;
            SoTienHoaHong = entity.SoTienCkNv ?? 0;
            NhapXuatId = entity.NhapXuatId ?? 0;
            KhoRongTon = entity.KhoRongTon ?? 0;
            ChieuDai = entity.ChieuDai ?? 0;
            TongChieuDai = entity.TongChieuDai ?? 0;
            TongDienTich = entity.TongDienTich ?? 0;
            GhiChu = entity.DienGiai;
            SoLuongTon = entity.SoLuongTon ?? 0;
            MaNhom = entity.MaNhom;
            TenNhom = entity.TenNhom;
            KieuSong = entity.KieuSong;
            MaKieuSong = entity.MaKieuSong;
            LoaiTon = entity.LoaiTon;
            MaLoaiTon = entity.MaLoaiTon;
            DoDay = entity.DoDay;
            ChungLoai = entity.ChungLoai;
            MaChungLoai = entity.MaChungLoai;
            MauSac = entity.MauSac;
            MaMauSac = entity.MaMauSac;
            TrangThaiDetail = entity.TrangThaiDetail ?? 0;
            SoTienCK = entity.SoTienCkNv;
            TyLeCK = entity.TyLeCK;
            IdId = entity.IdId;
            LoaiPhieu = entity.LoaiPhieu;
            SHTK = entity.SHTK;
            SoTienUSD = entity.SoTienUSD ?? 0;
            DonGiaUSD = entity.DonGiaUSD ?? 0;
            MaDonVi = entity.MaDonVi;
            TenDonVi = entity.TenDonVi;
            MaDonVi1 = entity.MaDonVi1;
            TenDonVi1 = entity.TenDonVi1;
            HanSuDung = entity.HanSuDung;
            KetChuyen1 = entity.KetChuyen1;
            GiaVon = entity.GiaVon ?? 0;
            GiaVonUSD = entity.GiaVonUSD ?? 0;
            MaPhanBo = entity.MaPhanBo;
            TenPhanBo = entity.TenPhanBo;
            Comment = entity.Comment;
            CapNhatGiaVon = entity.CapNhatGiaVon ?? false;
            TyLeChietKhau = entity.TyLeChietKhau ?? 0;
            SoTienChietKhau = entity.SoTienChietKhau ?? 0;
            SoTienXuat = entity.SoTienXuat ?? 0;
            DonGiaXuat = entity.DonGiaXuat ?? 0;
            ChenhLech = entity.ChenhLech ?? 0;
            XuatXu = entity.XuatXu;
            QuyCach = entity.QuyCach;
            MaLoai = entity.MaLoai;
            TenLoai = entity.TenLoai;
            PhuongThuc = entity.PhuongThuc;
            MaHangHoa1 = entity.MaHangHoa1;
            TenHangHoa1 = entity.TenHangHoa1;
            DonViTinh1 = entity.DonViTinh1;
            SoLuong1 = entity.SoLuong1 ?? 0;
            ThueNK = entity.ThueNK ?? 0;
            ThueVAT = entity.ThueVAT ?? 0;
            TyLePhiVanChuyen = entity.TyLePhiVanChuyen ?? 0;
            SoTienPhiVanChuyen = entity.SoTienPhiVanChuyen ?? 0;
            TyLeKhuyenMai = entity.TyLeKhuyenMai ?? 0;
            SoTienKhuyenMai = entity.SoTienKhuyenMai ?? 0;
            TongCong = entity.TongCong ?? 0;
            TinhChat = entity.TinhChat;
            Kieu = entity.Kieu;
            SoTienVND = entity.SoTienVND ?? 0;
            POLY = entity.POLY ?? 0;
            MDI = entity.MDI ?? 0;
            MaHoaChatPoly = entity.MaHoaChatPOLY;
            MaHoaChatMDI = entity.MaHoaChatMDI;
            MaDonViNhan = entity.MaDonViNhan;
            TenDonViNhan = entity.TenDonViNhan;
            NuocSanXuat = entity.NuocSanXuat;
            SoTienOk = entity.SoTienOk ?? 0;
            DonGiaBan = entity.DonGiaBan ?? 0;
            SoTienBan = entity.SoTienBan ?? 0;
            MaKho = entity.MaKho;
            ThoiGianTao = entity.ThoiGianTao;
            SoLuongTra = entity.SoLuongTra ?? 0;
            LoHang = entity.LoHang;
            MaBarCode = entity.MaBarCode;
            DonGiaBanLe = entity.DonGiaBanLe ?? 0;
            SoTienBanLe = entity.SoTienBanLe ?? 0;
            TyLeVAT = entity.TyLeVAT ?? 0;
            SoTienVAT = entity.SoTienVAT ?? 0;
            TyTrong = entity.TyTrong;
            DonGiaDVT1 = entity.DonGiaDVT1;
            SoLuongDVT1 = entity.SoLuongDVT1;
            SoTienDVT1 = entity.SoTienDVT1;
            TyLeCkNv = entity.TyLeCkNv;
            SoTienCkNv = entity.SoTienCkNv;
            SoPhieuLSX = entity.SoPhieuLSX;
            NgayPhieuLSX = entity.NgayPhieuLSX;
            SoPhieuXuat = entity.SoPhieuXuat;
            NgayXuatKho = entity.NgayXuatKho;
            NgayXacNhan = entity.NgayXacNhan;
        }
    }

    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
    [NotMapped] public int Index { get; set; }
    public int Id { get; set; }

    public string MaHangHoa { get; set; }
    [Required(ErrorMessage = "Bạn phải nhập vào tên hàng hóa")]
    public string TenHangHoa { get; set; }
    public string DonViTinh { get; set; }

    public double SoLuong { get; set; }

    public double DonGia { get; set; }

    public double SoTien { get; set; }

    public double DonGiaHoaHong { get; set; }

    public double SoTienHoaHong { get; set; }

    public int NhapXuatId { get; set; }


    public double KhoRongTon { get; set; }

    public double ChieuDai { get; set; }


    public double TongChieuDai { get; set; }

    //[DisplayFormat(DataFormatString = "{0:###,###.##", ApplyFormatInEditMode = true)]
    public double TongDienTich { get; set; }
    public int DMHangHoaId { get; set; }
    public int DMKhoHangId { get; set; }
    public int DMTinhGiaId { get; set; }

    public bool IsEditing { get; set; }

    public string GhiChu { get; set; }
    public string MaNhom { get; set; }
    public string TenNhom { get; set; }

    public string KieuSong { get; set; }
    public string MaKieuSong { get; set; }

    public string LoaiTon { get; set; }
    public string MaLoaiTon { get; set; }
    public string DoDay { get; set; }
    public string MaDoDay { get; set; }

    public string ChungLoai { get; set; }
    public string MaChungLoai { get; set; }


    public string MauSac { get; set; }
    public string MaMauSac { get; set; }
    public int TrangThaiDetail { get; set; }


    [NotMapped]
    public bool IsM2 { get; set; }
    [NotMapped]
    public bool IsMD { get; set; }
    [NotMapped]
    public bool IsSoLuong { get; set; }
    [NotMapped]
    public double SoLuongTon { get; set; }

    public int IdId { get; set; }

    public string Stt { get; set; }

    public double? TyLeCK { get; set; }

    public double? SoTienCK { get; set; }

    public double? SoConLai
    {
        get
        {
            return SoTien - SoTienCK;
        }
    }

    public string? LoaiPhieu { get; set; }

    public string? SHTK { get; set; }

    public double SoTienUSD { get; set; }

    public double DonGiaUSD { get; set; }

    public string? MaDonVi { get; set; }

    public string? TenDonVi { get; set; }

    public string? MaDonVi1 { get; set; }

    public string? TenDonVi1 { get; set; }

    public DateTime? HanSuDung { get; set; }

    public string? KetChuyen1 { get; set; }

    public double GiaVon { get; set; }

    public double GiaVonUSD { get; set; }

    public string? MaPhanBo { get; set; }

    public string? TenPhanBo { get; set; }

    public string? Comment { get; set; }

    public bool CapNhatGiaVon { get; set; }

    public double TyLeChietKhau { get; set; }

    public double SoTienChietKhau { get; set; }

    public double SoTienXuat { get; set; }

    public double DonGiaXuat { get; set; }

    public double ChenhLech { get; set; }

    public string? XuatXu { get; set; }

    public string? QuyCach { get; set; }

    public string? MaLoai { get; set; }

    public string? TenLoai { get; set; }

    public string? PhuongThuc { get; set; }

    public string? MaHangHoa1 { get; set; }

    public string? TenHangHoa1 { get; set; }

    public string? DonViTinh1 { get; set; }

    public double SoLuong1 { get; set; }

    public double ThueNK { get; set; }

    public double ThueVAT { get; set; }

    public double TyLePhiVanChuyen { get; set; }

    public double SoTienPhiVanChuyen { get; set; }

    public double TyLeKhuyenMai { get; set; }

    public double SoTienKhuyenMai { get; set; }

    public double TongCong { get; set; }

    public string? TinhChat { get; set; }

    public string? Kieu { get; set; }

    public double SoTienVND { get; set; }

    public double POLY { get; set; }

    public double MDI { get; set; }

    public string? MaHoaChatPoly { get; set; }

    public string? MaHoaChatMDI { get; set; }

    public string? MaDonViNhan { get; set; }

    public string? TenDonViNhan { get; set; }

    public string? NuocSanXuat { get; set; }

    public double SoTienOk { get; set; }

    public double DonGiaBan { get; set; }

    public double SoTienBan { get; set; }

    public string? MaKho { get; set; }

    public string? ThoiGianTao { get; set; }

    public double SoLuongTra { get; set; }

    public string? LoHang { get; set; }

    public string? MaBarCode { get; set; }

    public double DonGiaBanLe { get; set; }

    public double SoTienBanLe { get; set; }

    public double TyLeVAT { get; set; }

    public double SoTienVAT { get; set; }

    public double? TyTrong { get; set; }

    public double? DonGiaDVT1 { get; set; }

    public double? SoLuongDVT1 { get; set; }

    public double? SoTienDVT1 { get; set; }

    public double? TyLeCkNv { get; set; }

    public double? SoTienCkNv { get; set; }

    public string? SoPhieuLSX { get; set; }

    public DateTime? NgayPhieuLSX { get; set; }

    public string? SoPhieuXuat { get; set; }

    public DateTime? NgayXuatKho { get; set; }

    public DateTime? NgayXacNhan { get; set; }

    public NoiDungNhapXuatModel Clone()
    {
        return new NoiDungNhapXuatModel
        {
            Id = this.Id,
            MaHangHoa = this.MaHangHoa,
            TenHangHoa = this.TenHangHoa,
            DonViTinh = this.DonViTinh,
            SoLuong = this.SoLuong,
            DonGia = this.DonGia,
            SoTien = this.SoTien,
            DonGiaHoaHong = this.DonGiaHoaHong,
            SoTienHoaHong = this.SoTienHoaHong,
            NhapXuatId = this.NhapXuatId,
            KhoRongTon = this.KhoRongTon,
            ChieuDai = this.ChieuDai,
            TongChieuDai = this.TongChieuDai,
            TongDienTich = this.TongDienTich,
            DMHangHoaId = this.DMHangHoaId,
            DMKhoHangId = this.DMKhoHangId,
            DMTinhGiaId = this.DMTinhGiaId,
            IsEditing = this.IsEditing,
            GhiChu = this.GhiChu,
            MaNhom = this.MaNhom,
            TenNhom = this.TenNhom,
            KieuSong = this.KieuSong,
            MaKieuSong = this.MaKieuSong,
            LoaiTon = this.LoaiTon,
            MaLoaiTon = this.MaLoaiTon,
            DoDay = this.DoDay,
            MaDoDay = this.MaDoDay,
            ChungLoai = this.ChungLoai,
            MaChungLoai = this.MaChungLoai,
            MauSac = this.MauSac,
            MaMauSac = this.MaMauSac,
            TrangThaiDetail = this.TrangThaiDetail,
            IsM2 = this.IsM2,
            IsMD = this.IsMD,
            IsSoLuong = this.IsSoLuong,
            SoLuongTon = this.SoLuongTon,
            IdId = this.IdId,
            Stt = this.Stt,
            TyLeCK = this.TyLeCK,
            SoTienCK = this.SoTienCK,
            LoaiPhieu = this.LoaiPhieu,
            SHTK = this.SHTK,
            SoTienUSD = this.SoTienUSD,
            DonGiaUSD = this.DonGiaUSD,
            MaDonVi = this.MaDonVi,
            TenDonVi = this.TenDonVi,
            MaDonVi1 = this.MaDonVi1,
            TenDonVi1 = this.TenDonVi1,
            HanSuDung = this.HanSuDung,
            KetChuyen1 = this.KetChuyen1,
            GiaVon = this.GiaVon,
            GiaVonUSD = this.GiaVonUSD,
            MaPhanBo = this.MaPhanBo,
            TenPhanBo = this.TenPhanBo,
            Comment = this.Comment,
            CapNhatGiaVon = this.CapNhatGiaVon,
            TyLeChietKhau = this.TyLeChietKhau,
            SoTienChietKhau = this.SoTienChietKhau,
            SoTienXuat = this.SoTienXuat,
            DonGiaXuat = this.DonGiaXuat,
            ChenhLech = this.ChenhLech,
            XuatXu = this.XuatXu,
            QuyCach = this.QuyCach,
            MaLoai = this.MaLoai,
            TenLoai = this.TenLoai,
            PhuongThuc = this.PhuongThuc,
            MaHangHoa1 = this.MaHangHoa1,
            TenHangHoa1 = this.TenHangHoa1,
            DonViTinh1 = this.DonViTinh1,
            SoLuong1 = this.SoLuong1,
            ThueNK = this.ThueNK,
            ThueVAT = this.ThueVAT,
            TyLePhiVanChuyen = this.TyLePhiVanChuyen,
            SoTienPhiVanChuyen = this.SoTienPhiVanChuyen,
            TyLeKhuyenMai = this.TyLeKhuyenMai,
            SoTienKhuyenMai = this.SoTienKhuyenMai,
            TongCong = this.TongCong,
            TinhChat = this.TinhChat,
            Kieu = this.Kieu,
            SoTienVND = this.SoTienVND,
            POLY = this.POLY,
            MDI = this.MDI,
            MaHoaChatPoly = this.MaHoaChatPoly,
            MaHoaChatMDI = this.MaHoaChatMDI,
            MaDonViNhan = this.MaDonViNhan,
            TenDonViNhan = this.TenDonViNhan,
            NuocSanXuat = this.NuocSanXuat,
            SoTienOk = this.SoTienOk,
            DonGiaBan = this.DonGiaBan,
            SoTienBan = this.SoTienBan,
            MaKho = this.MaKho,
            ThoiGianTao = this.ThoiGianTao,
            SoLuongTra = this.SoLuongTra,
            LoHang = this.LoHang,
            MaBarCode = this.MaBarCode,
            DonGiaBanLe = this.DonGiaBanLe,
            SoTienBanLe = this.SoTienBanLe,
            TyLeVAT = this.TyLeVAT,
            SoTienVAT = this.SoTienVAT,
            TyTrong = this.TyTrong,
            DonGiaDVT1 = this.DonGiaDVT1,
            SoLuongDVT1 = this.SoLuongDVT1,
            SoTienDVT1 = this.SoTienDVT1,
            TyLeCkNv = this.TyLeCkNv,
            SoTienCkNv = this.SoTienCkNv,
            SoPhieuLSX = this.SoPhieuLSX,
            NgayPhieuLSX = this.NgayPhieuLSX,
            SoPhieuXuat = this.SoPhieuXuat,
            NgayXuatKho = this.NgayXuatKho,
            NgayXacNhan = this.NgayXacNhan,
        };
    }
}
