using QLSX.Shared.Entities;

namespace QLSX.Shared.Models;

public class DanhMucNhomKhachHangModel : BaseModel
{
    public DanhMucNhomKhachHangModel(DanhMucNhomKhachHang entity)
    {
        if (entity != null)
        {
            Id = entity.Id;
            MaNhom = entity.MaNhom;
            TenNhom = entity.TenNhom;
            GhiChu = entity.GhiChu;
            //KyHieu = entity.KyHieu;
            Selected = (bool)entity.Selected;
        }
    }

    public DanhMucNhomKhachHangModel()
    {
    }

    public int Id { get; set; }
    public string MaNhom { get; set; }
    public string TenNhom { get; set; }
    public string GhiChu { get; set; }
    public string KyHieu { get; set; }
    public bool Selected { get; set; }
}
