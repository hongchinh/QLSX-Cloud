using QLSX.Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Models;

public class DanhMucKhachHangModel : BaseModel
{
    public DanhMucKhachHangModel(DanhMucKhachHang entity, DanhMucNhomKhachHang nhomKhach, DanhMucTinhThanh tinhThanh)
    {
        if (entity != null)
        {
            Id = entity.Id;
            MaDonVi = entity.MaDonVi;
            TenDonVi = entity.TenDonVi;
            DienThoai = entity.DienThoai;
            //DienThoai1 = entity.DienThoai1;
            DiaChi = entity.DiaChi;
            MaTinh = entity.MaTinh;
            MaNhom = entity.MaNhom;
            SoTaiKhoan = entity.SoTaiKhoan;
            TenNganHang = entity.NoiMoTaiKhoan;
            MaSoThue = entity.MaSoThue;
            Website = entity.Website;
            HanMucDuNo = (double?)entity.HanMucDuNo;
            //KhongTheoDoi = entity.KhongTheoDoi;
            //UserId = entity.UserId;
            DMNhomKhachHangs = new DanhMucNhomKhachHangModel(nhomKhach);
            DMTinhThanhs = new DanhMucTinhThanhModel(tinhThanh);
        }
    }

    public DanhMucKhachHangModel(DanhMucKhachHang entity)
    {
        if (entity != null)
        {
            Id = entity.Id;
            MaDonVi = entity.MaDonVi;
            TenDonVi = entity.TenDonVi;
            DienThoai = entity.DienThoai;
            DiaChi = entity.DiaChi;
            MaTinh = entity.MaTinh;
            MaNhom = entity.MaNhom;
            SoTaiKhoan = entity.SoTaiKhoan;
            TenNganHang = entity.NoiMoTaiKhoan;
            MaSoThue = entity.MaSoThue;
            Website = entity.Website;
            HanMucDuNo = (double?)entity.HanMucDuNo;
        }
    }

    public DanhMucKhachHangModel()
    {
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "Bạn phải nhập vào Mã đơn vị")]
    public string MaDonVi { get; set; }

    [Required(ErrorMessage = "Bạn phải nhập vào Tên đơn vị")]
    public string TenDonVi { get; set; }
    public string? DienThoai { get; set; }
    public string? DienThoai1 { get; set; }

    public string? DiaChi { get; set; }

    public string MaTinh { get; set; }

    public string MaNhom { get; set; }

    [NotMapped]
    public string? TenNhom
    {
        get
        {
            return DMNhomKhachHangs?.TenNhom ?? string.Empty;
        }
    }
    public DanhMucNhomKhachHangModel DMNhomKhachHangs { get; set; }
    public DanhMucTinhThanhModel DMTinhThanhs { get; set; }

    public string? SoTaiKhoan { get; set; }
    public string? TenNganHang { get; set; }
    public string? MaSoThue { get; set; }
    public string? Website { get; set; }
    public double? HanMucDuNo { get; set; }
    public bool? KhongTheoDoi { get; set; }
    public int? UserId { get; set; }
    public UserModel User { get; set; }
}
