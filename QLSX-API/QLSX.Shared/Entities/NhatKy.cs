using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("NHATKY")]
public class NhatKy : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("NGAY")]
    public DateTime? Ngay { get; set; }

    [Column("HOTEN")]
    public string? HoTen { get; set; }

    [Column("CHUCNANG")]
    public string? ChucNang { get; set; }

    [Column("GHICHU")]
    public string? GhiChu { get; set; }

    [Column("SOCHUNGTU")]
    public string? SoChungTu { get; set; }

    [Column("IDPHIEU")]
    public string? IdPhieu { get; set; }

}
