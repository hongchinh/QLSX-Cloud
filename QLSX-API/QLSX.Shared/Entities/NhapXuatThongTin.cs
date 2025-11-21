using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("NHAPXUAT_THONGTIN")]
public class NhapXuatThongTin
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("LOAIPHIEU")]
    public string? LoaiPhieu { get; set; }

    [Column("SOTIEN_GIAM")]
    public double? SoTienGiam { get; set; }

    [Column("SOTIEN_CK")]
    public double? SoTienCK { get; set; }

    [Column("SOTIEN_VC")]
    public double? SoTienVc { get; set; }

    [Column("TYLE_VAT")]
    public double? TyLeVat { get; set; }

    [Column("SOTIEN_VAT")]
    public double? SoTienVat { get; set; }

    [Column("SOTIEN_TT")]
    public double? SoTienTT { get; set; }

    [Column("SOTIEN")]
    public double? SoTien { get; set; }

    [Column("NGAYCT")]
    public DateTime? NgayCt { get; set; }

    [Column("SOCT")]
    public string? SoCt { get; set; }

    [Column("TONGCONG")]
    public double? TongCong { get; set; }

    [Column("CREATEDDATE")]
    public DateTime? CreatedDate { get; set; }

    [Column("UPDATEDDATE")]
    public DateTime? UpdatedDate { get; set; }

    [Column("GHICHU")]
    public string? GhiChu { get; set; }

    [Column("CreateBy")]
    public string? CreateBy { get; set; }

    [Column("UpdateBy")]
    public string? UpdateBy { get; set; }

    [Column("IDID")]
    public string? IdId { get; set; }
}
