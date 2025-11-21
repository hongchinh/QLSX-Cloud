using QLSX.Shared.Entities;

namespace QLSX.Shared.Models;

public class DanhMucHinhThucTTModel : BaseModel
{
    public DanhMucHinhThucTTModel(DanhMucHinhThucTT enity)
    {
        Id = enity.Id;
        TenHinhThuc = enity.ChiTieu;
        GhiChu = enity.GhiChu;
    }

    public DanhMucHinhThucTTModel()
    {
    }

    public int Id { get; set; }

    public string TenHinhThuc { get; set; }

    public string GhiChu { get; set; }
}
