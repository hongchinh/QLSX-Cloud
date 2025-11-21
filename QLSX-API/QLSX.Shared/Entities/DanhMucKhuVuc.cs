using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

//[Table("DANHMUCKHUVUC")]
public class DanhMucKhuVuc : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("STT")]
    public int? Stt { get; set; }

    [Column("MAKHUVUC")]
    public string? MaKhuVuc { get; set; }

    [Column("TENKHUVUC")]
    public string? TenKhuVuc { get; set; }

    [Column("MANHOMKHUVUC")]
    public string? MaNhomKhuVuc { get; set; }

    [Column("TENNHOMKHUVUC")]
    public string? TenNhomKhuVuc { get; set; }

    [Column("GHICHU")]
    public string? GhiChu { get; set; }

    [Column("SELECTTED")]
    public bool? Selected { get; set; }

    [Column("KYHIEU")]
    public string? KyHieu { get; set; }

    [Column("MLONG")]
    public string? MLONG { get; set; }

    [Column("MLAT")]
    public string? MLAT { get; set; }
}
