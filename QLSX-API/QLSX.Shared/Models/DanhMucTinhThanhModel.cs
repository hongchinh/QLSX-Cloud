using QLSX.Shared.Entities;

namespace QLSX.Shared.Models;

public class DanhMucTinhThanhModel : BaseModel
{
    public DanhMucTinhThanhModel(DanhMucTinhThanh entity)
    {
        if (entity != null)
        {
            Id = entity.Id;
            MaTinh = entity.MaKhuVuc;
            TenTinh = entity.TenKhuVuc;
        }
    }

    public DanhMucTinhThanhModel()
    {
    }

    public int Id { get; set; }
    public string MaTinh { get; set; }
    public string TenTinh { get; set; }
}
