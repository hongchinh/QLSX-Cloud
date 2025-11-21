using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("LISTNHOMHANG")]
public class ListNhomHang : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("STT")]
    public int? Stt { get; set; }

    [Column("MANHOMHANG")]
    public string? MaNhomHang { get; set; }

    [Column("TENNHOMHANG")]
    public string? TenNhomHang { get; set; }

    [Column("CAP")]
    public int? Cap { get; set; }

    [Column("GHICHU")]
    public string? GhiChu { get; set; }

    [Column("SELECTTED")]
    public bool? Selected { get; set; }
}
