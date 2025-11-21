using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace QLSX.Shared.Models;

public class DonDatHangModel : BaseModel
{
    public string LoaiPhieu { get; set; }

    public string MaDoiTuong { get; set; }

    public string TenDoiTuong { get; set; }

    public string DiaChiDoiTuong { get; set; }

    public string MaKho { get; set; }

    public string TenKho { get; set; }

    public string GhiChu { get; set; }

    public string SoChungTu { get; set; }

    public bool Selected { get; set; }

    public string MaDonViSuDung { get; set; }

    public string TenDonViSuDung { get; set; }
    public string Phieu { get; set; }

    public int Id { get; set; }

    public string Loai { get; set; }

    [Required(ErrorMessage = "Bạn phải nhập vào ngày lập phiếu")]
    public DateTime? NgayCT { get; set; }

    [Required(ErrorMessage = "Bạn phải nhập vào ngày hoàn thành")]
    public DateTime? NgayHT { get; set; }

    [Required(ErrorMessage = "Bạn phải nhập vào số phiếu")]
    public string SoCT { get; set; }

    [Required(ErrorMessage = "Bạn phải nhập vào mã đơn vị")]
    public string MaDonVi { get; set; }

    [Required(ErrorMessage = "Bạn phải nhập vào tên đơn vị")]
    public string TenDonVi { get; set; }
    public string DiaChi { get; set; }
    public string DienThoai { get; set; }
    public bool ThuongMai { get; set; }
    public bool ThiCong { get; set; }
    public int? MaNhanVienId { get; set; }
    public int? MaDonViId { get; set; }
    public int MaLyDoId { get; set; }
    public int? UserId { get; set; }


    public UserModel User { get; set; }

    [Required(ErrorMessage = "Bạn phải nhập vào kho hàng")]
    public int DMKhoHangId { get; set; }

    public DanhMucKhoHangModel DMKhoHang { get; set; }
    public DateTime? NgayHenThanhToan { get; set; }
    public DateTime? NgayGiao { get; set; }
    public string NoiGiao { get; set; }
    public string PhuongTien { get; set; }
    public int? DMHinhThucTTId { get; set; }
    public int DMTinhTrangId { get; set; }
    public double TyleVAT { get; set; }

    public bool TuThiCong { get; set; }
    public string DienGiai { get; set; }

    public bool ThueNgoai { get; set; }
    public double? TyLeThiCong { get; set; }
    public double? SoTienTT { get; set; }

    public int? NVKyThuatId { get; set; }

    public int? DMKhachHangId { get; set; }
    public int? DMLoaiTienId { get; set; }
    public DanhMucLoaiTienModel DMLoaiTiens { get; set; }

    public bool? SanPhamTheoM2 { get; set; }

    [NotMapped]
    private double? _sotien { get; set; }
    [NotMapped]
    public double? SoTien
    {
        get { return this.NoiDungDonDatHangs?.Sum(x => x.SoTien); }
        set { _sotien = value; }
    }

    [ValidateComplexType]
    public List<NoiDungDonDatHang> NoiDungDonDatHangs { get; set; } = new List<NoiDungDonDatHang>();
}
