using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("BAOCAO")]
public class BaoCao : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("STT")]
    public int? Stt { get; set; }

    [Column("MASO")]
    public string? MaSo { get; set; }

    [Column("TENBAOCAO")]
    public string? TenBaoCao { get; set; }

    [Column("MALOAIBAOCAO")]
    public string? MaLoaiBaoCao { get; set; }

    [Column("TENLOAIBAOCAO")]
    public string? TenLoaiBaoCao { get; set; }

    [Column("REPORTFILES")]
    public string? ReportFiles { get; set; }

    [Column("EXCELFILES")]
    public string? ExcelFiles { get; set; }

    [Column("LOAI")]
    public string? Loai { get; set; }

    [Column("SELECTTED")]
    public bool? Selected { get; set; }
}
