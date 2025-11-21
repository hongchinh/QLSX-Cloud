using QLSX.Shared.Entities;

namespace QLSX.Shared.Models;

public class DanhMucChungLoaiModel
{
    public DanhMucChungLoaiModel(DanhMucChungLoai entity)
    {
        if (entity != null)
        {
            Id = entity.Id;
            MaSo = entity.MaSo;
            ChiTieu = entity.ChiTieu;
        }
    }

    public DanhMucChungLoaiModel()
    {
        MaSo = string.Empty;
        ChiTieu = string.Empty;
    }

    public int Id { get; set; }
    public string MaSo { get; set; }
    public string ChiTieu { get; set; }

}
