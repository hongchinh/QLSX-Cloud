using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("DANHMUCNHOMDONVI")]

public class DanhMucNhomKhachHang : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("MANHOM")]
    public string? MaNhom { get; set; }

    [Column("TENNHOM")]
    public string? TenNhom { get; set; }

    [Column("GHICHU")]
    public string? GhiChu { get; set; }

    [Column("SELECTTED")]
    public bool? Selected { get; set; }

    [Column("MANHOMCAP2")]
    public string? MaNhomCap2 { get; set; }

    [Column("SELECTTEDCK")]
    public bool? SelectedCk { get; set; }
}

