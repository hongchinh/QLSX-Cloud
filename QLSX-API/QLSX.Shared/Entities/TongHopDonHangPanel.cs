using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Entities;

[Table("TONGHOPDONHANG_PANEL")]
public class TongHopDonHangPanel : BaseModel
{
    [Column("ID")]
    public int Id { get; set; }


    [Column("NGAYCT")]
    public DateTime NgayCt { get; set; }


    [Column("DODAY")]
    public string DoDay { get; set; }


    [Column("KHORONG")]
    public string KhoRong { get; set; }


    [Column("MAUSAC")]
    public string MauSac { get; set; }


    [Column("TONGCONG")]
    public double TongCong { get; set; }


    [Column("GHICHU")]
    public string GhiChu { get; set; }


    [Column("CHUNGLOAI")]
    public string ChungLoai { get; set; }
}
