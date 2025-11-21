using QLSX.Shared.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLSX.Shared.Models;

public class DanhMucKhoHangModel
{
    public DanhMucKhoHangModel(DanhMucKhoHang entity)
    {
        if (entity != null)
        {
            Id = entity.Id;
            MaKho = entity.MaKho;
            TenKho = entity.TenKho;
            DiaChi = entity.DiaChi;
        }
    }

    public DanhMucKhoHangModel()
    {
        MaKho = string.Empty;
        TenKho = string.Empty;
        DiaChi = string.Empty;
    }

    public int Id { get; set; }

    //[Required(ErrorMessage = "Bạn phải nhập vào Mã kho")]
    public string MaKho { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Bạn phải nhập vào tên kho")]

    public string TenKho { get; set; } = string.Empty;

    public string DiaChi { get; set; } = string.Empty;

    public List<NhapXuatModel> NhapXuats { get; set; } = new List<NhapXuatModel>();
}
