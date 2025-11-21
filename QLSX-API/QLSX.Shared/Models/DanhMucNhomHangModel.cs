using QLSX.Shared.Entities;

namespace QLSX.Shared.Models;

public class DanhMucNhomHangModel
{
    public DanhMucNhomHangModel(DanhMucNhomHang entity)
    {
        if (entity != null)
        {
            Id = entity.Id;
            MaNhom = entity.MaNhomHang;
            TenNhom = entity.TenNhomHang;
            GhiChu = entity.GhiChu;
            KyHieu = entity.KyHieu;
            Selected = (bool)entity.Selected;
        }
    }

    public DanhMucNhomHangModel()
    {
        MaNhom = string.Empty;
        TenNhom = string.Empty;
        GhiChu = string.Empty;
        //KyHieu = kyHieu;
        //Selected = selected;
    }

    public int Id { get; set; }

    public string MaNhom { get; set; }

    public string TenNhom { get; set; }

    public string GhiChu { get; set; }

    public string KyHieu { get; set; }

    public bool Selected { get; set; }
}
