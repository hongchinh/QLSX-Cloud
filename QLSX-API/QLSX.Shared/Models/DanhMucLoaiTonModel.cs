using QLSX.Shared.Entities;

namespace QLSX.Shared.Models;

public class DanhMucLoaiTonModel : BaseModel
{
    public DanhMucLoaiTonModel(DanhMucLoaiTon entity)
    {
        if (entity != null)
        {
            Id = entity.Id;
            MaSo = entity.MaSo;
            ChiTieu = entity.ChiTieu;
        }
    }

    public DanhMucLoaiTonModel()
    {
    }

    public int Id { get; set; }

    public string MaSo { get; set; }

    public string ChiTieu { get; set; }

}
