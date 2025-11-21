using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("DANHMUCLOAITIEN")]
public class DanhMucLoaiTien : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("STT")]
    public int? Stt { get; set; }

    [Column("KYHIEU")]
    public string? KyHieu { get; set; }

    [Column("LOAITIEN")]
    public string? LoaiTien { get; set; }

    [Column("TYGIA1")]
    public double? TyGia1 { get; set; }

    [Column("TYGIA2")]
    public double? TyGia2 { get; set; }

    [Column("TYGIA3")]
    public double? TyGia3 { get; set; }

    [Column("TYGIA4")]
    public double? TyGia4 { get; set; }

    [Column("TYGIA5")]
    public double? TyGia5 { get; set; }

    [Column("TYGIA6")]
    public double? TyGia6 { get; set; }

    [Column("TYGIA7")]
    public double? TyGia7 { get; set; }

    [Column("TYGIA8")]
    public double? TyGia8 { get; set; }

    [Column("TYGIA9")]
    public double? TyGia9 { get; set; }

    [Column("TYGIA10")]
    public double? TyGia10 { get; set; }

    [Column("TYGIA11")]
    public double? TyGia11 { get; set; }

    [Column("TYGIA12")]
    public double? TyGia12 { get; set; }
}
