using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("DANHMUCKHOVATTU")]
public class DanhMucKhoHang : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("STT")]
    public int? Stt { get; set; }

    [Column("MAKHO")]
    public string MaKho { get; set; }

    [Column("TENKHO")]
    public string? TenKho { get; set; }

    [Column("DIACHI")]
    public string? DiaChi { get; set; }

    [Column("GHICHU")]
    public string? GhiChu { get; set; }

    [Column("THUKHO")]
    public string? ThuKho { get; set; }

    [Column("SELECTTED")]
    public bool? Selected { get; set; }

    [Column("TYLEVAT")]
    public double? TyLeVat { get; set; }
}
