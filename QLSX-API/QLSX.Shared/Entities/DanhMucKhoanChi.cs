using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("DANHMUCKHOANCHI")]
public class DanhMucKhoanChi : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("STT")]
    public int? Stt { get; set; }

    [Column("MASO")]
    public string MaSo { get; set; }

    [Column("CHITIEU")]
    public string? ChiTieu { get; set; }

    [Column("CAP")]
    public int? Cap { get; set; }

    [Column("SHTK")]
    public string? SHTK { get; set; }

    [Column("BATBUOC")]
    public bool? BatBuoc { get; set; }

    [Column("GHICHU")]
    public string? GhiChu { get; set; }
}
