using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("QUYENSUDUNG")]
public class QuyenSuDung : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("STT")]
    public int? Stt { get; set; }

    [Column("MASO")]
    public string? MaSo { get; set; }

    [Column("CHUCNANG")]
    public string? ChucNang { get; set; }

    [Column("HOTEN")]
    public string? HoTen { get; set; }

    [Column("SELECTTED")]
    public bool? Selectted { get; set; }

    [Column("THEM")]
    public bool? Them { get; set; }

    [Column("XOA")]
    public bool? Xoa { get; set; }

    [Column("SUA")]
    public bool? Sua { get; set; }

    [Column("XEMIN")]
    public bool? XemIn { get; set; }

    [Column("UserId")]
    public int? UserId { get; set; }
}
