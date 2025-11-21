using QLSX.Shared.Entities;
using System;
 

namespace QLSX.Shared.Models;

public class NhatKyModel
{
    public NhatKyModel(NhatKy entity)
    {
        Id = entity.Id;
        ChucNang=entity.ChucNang;
        Ngay = entity.Ngay ?? DateTime.MinValue ;
        HoTen = entity.HoTen;
        GhiChu = entity.GhiChu;
        ThoiGian = entity.CreatedDate;
        SoChungTu= entity.SoChungTu;
        IdPhieu=entity.IdPhieu;
    }
    public NhatKyModel()
    {
    }
    public int Id { get; set; }
    public string ChucNang { get; set; }
    public DateTime Ngay { get; set; }
    public DateTime ThoiGian { get; set; }
    public int UserId { get; set; }
    public string HoTen { get; set; }
    public string SoChungTu { get; set; }
    public string IdPhieu { get; set; }
    public string GhiChu { get; set; }
   

}
