using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("DANHMUCNHOMVATTU")]
public class DanhMucNhomHang : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("STT")]
    public int? STT { get; set; }

    [Column("MANHOMHANG")]
    public string? MaNhomHang { get; set; }

    [Column("TENNHOMHANG")]
    public string? TenNhomHang { get; set; }

    [Column("GHICHU")]
    public string? GhiChu { get; set; }

    [Column("SELECTTED")]
    public bool? Selected { get; set; }

    [Column("TRUYENTHONG")]
    public string? TruyenThong { get; set; }

    [Column("TYLEVAT")]
    public int? TyLeVAT { get; set; }

    [Column("MANHOMHANGCAP2")]
    public string? MaNhomHangCap2 { get; set; }

    [Column("manhom")]
    public string? MaNhom { get; set; }

    [Column("KYHIEU")]
    public string? KyHieu { get; set; }
}
