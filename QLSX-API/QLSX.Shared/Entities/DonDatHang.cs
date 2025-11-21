using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("DONDATHANG")]
public class DonDatHang : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("LOAIPHIEU")]
    public string? LoaiPhieu { get; set; }

    [Column("MADOITUONG")]
    public string? MaDoiTuong { get; set; }

    [Column("TENDOITUONG")]
    public string? TenDoiTuong { get; set; }

    [Column("DIACHIDOITUONG")]
    public string? DiaChiDoiTuong { get; set; }

    [Column("MAKHO")]
    public string? MaKho { get; set; }

    [Column("TENKHO")]
    public string? TenKho { get; set; }

    [Column("GHICHU")]
    public string? GhiChu { get; set; }

    [Column("SOCHUNGTU")]
    public string? SoChungTu { get; set; }

    [Column("NGAYCT")]
    public DateTime? NgayCT { get; set; }

    [Column("SELECTTED")]
    public bool? Selected { get; set; }

    [Column("MADONVISUDUNG")]
    public string? MaDonViSuDung { get; set; }

    [Column("TENDONVISUDUNG")]
    public string? TenDonViSuDung { get; set; }

    [Column("LOAI")]
    public string? Loai { get; set; }

    [Column("PHIEU")]
    public string? Phieu { get; set; }
}
