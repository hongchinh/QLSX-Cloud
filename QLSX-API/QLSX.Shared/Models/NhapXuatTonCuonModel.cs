using QLSX.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace QLSX.Shared.Models;

public class NhapXuatTonCuonModel : BaseModel
{
    public NhapXuatTonCuonModel()
    {
    }

    public NhapXuatTonCuonModel(NhapXuatTonCuon entity)
    {
        Id = entity.Id;
        Loai = entity.Loai;
        NgayCT = entity.NgayCT ?? DateTime.Now;
        NgayHT = entity.NgayGhi ?? DateTime.Now;
        LoaiPhieu = entity.LoaiPhieu;
        SoChungTu = entity.SoChungTu;
        MaDonVi = entity.MaDoiTuong;
        TenDonVi = entity.TenDoiTuong;
        DiaChi = entity.DiaChiDoiTuong;
        MaDonHang = entity.MaKho;
        NgayGiao = entity.NgayGiao;
        NoiGiao = entity.NoiGiaoHang;
        MaDonHang = entity.MaDonViSuDung;
        TyleVAT = (double)entity.TyLeVAT;
        DienGiai = entity.DienGiai;
        TongCong = entity.TongCong ?? 0;
        MaQuanLy = entity.MaQuanLy;
        TenQuanLy = entity.TenQuanLy;
        MaTinh = entity.MaTinh;
        TenTinh = entity.TenTinh;
        LoaiTien = entity.LoaiTien;
        int.TryParse(entity.HinhThucTT, out int dmHinhThucTTId);
        DMHinhThucTTId = dmHinhThucTTId;
        SoTienTT = entity.SoTienTT ?? 0;
        MaLyDo = entity.MaLyDo;
        TenLyDo = entity.TenLyDo;
        NgayHenThanhToan = entity.HanThanhToan;
    }

    public NhapXuatTonCuonModel(NhapXuatTonCuon entity, List<NoiDungNhapXuatTonCuon> noiDungNhapXuatList, DanhMucKhoHang danhMucKhoHang, DanhMucLoaiTien danhMucLoaiTien)
    {
        Id = entity.Id;
        Loai = entity.Loai;
        NgayHT = entity.NgayGhi ?? DateTime.Now;
        NgayCT = entity.NgayCT ?? DateTime.Now;
        SoChungTu = entity.SoChungTu;
        MaDonVi = entity.MaDoiTuong;
        TenDonVi = entity.TenDoiTuong;
        DiaChi = entity.DiaChiDoiTuong;
        LoaiPhieu = entity.LoaiPhieu;
        NgayGiao = entity.NgayGiao;
        NoiGiao = entity.NoiGiaoHang;
        MaDonHang = entity.MaDonViSuDung;
        TyleVAT = (double)entity.TyLeVAT;
        DienGiai = entity.DienGiai;
        TongCong = entity.TongCong ?? 0;
        NoiDungNhapXuatTonCuons = noiDungNhapXuatList.Where(item => item.LoaiPhieu == entity.LoaiPhieu).Select(item => new NoiDungNhapXuatTonCuonModel(item)).ToList();
        DMKhoHang = new DanhMucKhoHangModel(danhMucKhoHang);
        DMLoaiTiens = new DanhMucLoaiTienModel(danhMucLoaiTien);
        DMLoaiTienId = DMLoaiTiens?.Id ?? 0;
        MaQuanLy = entity.MaQuanLy;
        TenQuanLy = entity.TenQuanLy;
        MaTinh = entity.MaTinh;
        TenTinh = entity.TenTinh;
        LoaiTien = entity.LoaiTien;
        int.TryParse(entity.HinhThucTT, out int dmHinhThucTTId);
        DMHinhThucTTId = dmHinhThucTTId;
        SoTienTT = entity.SoTienTT ?? 0;
        MaLyDo = entity.MaLyDo;
        MaKho = entity.MaKho;
        PhuongTien = entity.HinhThucGiaoHang;
        TenLyDo = entity.TenLyDo;
        NgayHenThanhToan = entity.HanThanhToan;
    }

    public int Id { get; set; }
    public string Loai { get; set; }
    public string LoaiTien { get; set; }
    public string LoaiPhieu { get; set; }
    public string LoaiDisplay
    {
        get
        {
            switch (Loai?.ToLower())
            {
                case "nhap":
                    return "Phiếu nhập";
                case "xuat":
                    return "Phiếu xuất";
                case "nhaptra":
                    return "Phiếu nhập trả";
                case "xuattra":
                    return "Phiếu xuất trả";
                case "dieuchuyen":
                    return "Điều chuyển";
                case "donhang":
                    return "Đơn hàng";
                default:
                    return Loai;
            };
        }
    }

    [Required(ErrorMessage = "Bạn phải nhập vào ngày lập phiếu")]

    public DateTime? NgayCT { get; set; }

    //[Required(ErrorMessage = "Bạn phải nhập vào ngày hoàn thành")]
    public DateTime? NgayHT { get; set; }

    [Required(ErrorMessage = "Bạn phải nhập vào số phiếu")]
    public String SoChungTu { get; set; }
    [Required(ErrorMessage = "Bạn phải nhập vào mã đơn vị")]
    public String? MaDonVi { get; set; }

    [Required(ErrorMessage = "Bạn phải nhập vào tên đơn vị")]
    public String TenDonVi { get; set; }
    public String DiaChi { get; set; }
    public String DienThoai { get; set; }
    public int? UserId { get; set; }

    public string MaKho { get; set; }
    public string HinhThucThanhToan { get; set; }

    public User User { get; set; }

    public int DMKhoHangId { get => DMKhoHang?.Id ?? 0; }

    public DateTime? NgayHenThanhToan { get; set; }
    public DateTime? NgayGiao { get; set; }
    public string NoiGiao { get; set; }
    public string? MaDonHang { get; set; }
    public string PhuongTien { get; set; }
    public int? DMHinhThucTTId { get; set; }
    public int DMTinhTrangId { get; set; }
    public double TyleVAT { get; set; }
    public string DienGiai { get; set; }
    public double SoTienTT { get; set; }
    public double TongCong { get; set; }
    public int DMLoaiTienId { get; set; }
    public bool? SanPhamTheoM2 { get; set; }
    public DanhMucKhoHangModel DMKhoHang { get; set; }

    public DanhMucLoaiTienModel DMLoaiTiens { get; set; }

    public string TenKho { get => DMKhoHang?.TenKho ?? string.Empty; }

    public string MaQuanLy { get; set; }
    public string TenQuanLy { get; set; }
    public string MaTinh { get; set; }
    public string TenTinh { get; set; }

    public string MaLyDo { get; set; }
    public string TenLyDo { get; set; }



    [ValidateComplexType]
    public List<NoiDungNhapXuatTonCuonModel> NoiDungNhapXuatTonCuons { get; set; } = new List<NoiDungNhapXuatTonCuonModel>();

    [NotMapped]
    public virtual List<tblFileAttachment> tblFileAttachments { get; set; } = new List<tblFileAttachment>();
    [NotMapped]
    public virtual ImageQRCode ImageQRCode { get; set; } = new ImageQRCode();

}
