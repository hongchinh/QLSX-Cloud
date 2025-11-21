using QLSX.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace QLSX.Shared.Models;

public class DanhMucLoaiTienModel
{
    public DanhMucLoaiTienModel(DanhMucLoaiTien? entity)
    {
        if (entity != null)
        {
            Id = entity.Id;
            LoaiTien = entity.LoaiTien;
            KyHieu = entity.KyHieu;
        }
    }

    public DanhMucLoaiTienModel()
    {
    }

    public int Id { get; set; }
    public string LoaiTien { get; set; }
    public string KyHieu { get; set; }
    public string GhiChu { get; set; }

}
