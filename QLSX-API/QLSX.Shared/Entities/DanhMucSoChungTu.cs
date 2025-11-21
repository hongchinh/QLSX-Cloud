using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("DANHMUCSOCHUNGTU")]
public class DanhMucSoChungTu : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("STT")]
    public int? STT { get; set; }

    [Column("LOAICHUNGTU")]
    public string? LoaiChungTu { get; set; }

    [Column("KYHIEUCHUNGTU")]
    public string? KyHieuChungTu { get; set; }

    [Column("DODAI")]
    public int? DoDai { get; set; }

    [Column("GHICHU")]
    public string? GhiChu { get; set; }
}

