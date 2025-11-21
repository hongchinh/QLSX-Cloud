using QLSX.Shared.Entities;
namespace QLSX.Shared.Models;

public class DanhMucKhoanThuModel
{
    public DanhMucKhoanThuModel(DanhMucKhoanThu model)
    {
        Id = model.Id;
        MaKhoanThu = model.MaSo;
        TenKhoanThu = model.ChiTieu;
        GhiChu = model.GhiChu;
    }

    public DanhMucKhoanThuModel()
    {
        MaKhoanThu = string.Empty;
        TenKhoanThu = string.Empty;
        GhiChu = string.Empty;
    }

    public int Id { get; set; }

    public string MaKhoanThu { get; set; }

    public string TenKhoanThu { get; set; }

    public string GhiChu { get; set; }
}
