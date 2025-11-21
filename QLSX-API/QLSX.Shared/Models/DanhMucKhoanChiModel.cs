using QLSX.Shared.Entities;

namespace QLSX.Shared.Models;

public class DanhMucKhoanChiModel
{
    public DanhMucKhoanChiModel(DanhMucKhoanChi model)
    {
        Id = model.Id;
        MaKhoanChi = model.MaSo;
        TenKhoanChi = model.ChiTieu;
        GhiChu = model.GhiChu;
    }
    public DanhMucKhoanChiModel()
    {
        MaKhoanChi = string.Empty;
        TenKhoanChi = string.Empty;
        GhiChu = string.Empty;
    }

    public int Id { get; set; }
    public string MaKhoanChi { get; set; }
    public string TenKhoanChi { get; set; }
    public string GhiChu { get; set; }
    
}
