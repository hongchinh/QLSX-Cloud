using QLSX.Shared.Entities;

namespace QLSX.Shared.Models;

public class DanhMucMauSacModel
{
    public DanhMucMauSacModel(DanhMucMauSac entity)
    {
        if (entity != null)
        {
            Id = entity.Id;
            MaSo = entity.MaSo;
            ChiTieu = entity.ChiTieu;
        }
    }

    public DanhMucMauSacModel()
    {
        MaSo = string.Empty;
        ChiTieu = string.Empty;
    }

    public int Id { get; set; }
    public string MaSo { get; set; }
    public string ChiTieu { get; set; }

}
