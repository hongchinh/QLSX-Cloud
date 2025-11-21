using QLSX.Shared.Entities;

namespace QLSX.Shared.Models;

public class DanhMucSoChungTuModel : BaseModel
{
    public DanhMucSoChungTuModel()
    {
    }

    public DanhMucSoChungTuModel(DanhMucSoChungTu entity)
    {
        Id = entity.Id;
        LoaiCT = entity.LoaiChungTu;
        KyHieu = entity.KyHieuChungTu;
        DoDai = entity.DoDai ?? 0;
        GhiChu = entity.GhiChu;
    }

    public int Id { get; set; }
    public string LoaiCT { get; set; }
    public string KyHieu { get; set; }
    public int DoDai { get; set; }
    public string GhiChu { get; set; }

}
