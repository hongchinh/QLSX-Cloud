using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("PHANQUYENBAOCAO")]
public class PhanQuyenBaoCao : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("TENBAOCAO")]
    public string? TenBaoCao { get; set; }

    [Column("HOTEN")]
    public string? HoTen { get; set; }

    [Column("SELECTTED")]
    public bool? Selected { get; set; }

    [Column("REPORTFILES")]
    public string? ReportFiles { get; set; }

    [Column("IDBAOCAO")]
    public int? IdBaoCao { get; set; }

    [Column("STT")]
    public int? Stt { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }


    [Column("LoaiBaoCao")]
    public string? LoaiBaoCao { get; set; }
}
