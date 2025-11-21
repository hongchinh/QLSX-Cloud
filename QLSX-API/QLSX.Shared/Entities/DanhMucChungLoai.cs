using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("DANHMUCCHUNGLOAI")]
public class DanhMucChungLoai : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("STT")]
    public int? Stt { get; set; }

    [Column("MASO")]
    public string? MaSo { get; set; }

    [Column("CHITIEU")]
    public string? ChiTieu { get; set; }

    [Column("GHICHU")]
    public string? GhiChu { get; set; }
}
