using QLSX.Shared.Entities;
using System;

namespace QLSX.Shared.Models;

public class TongHopDonHangPanelModel
{
    public TongHopDonHangPanelModel(TongHopDonHangPanel entity)
    {
        Id = entity.Id;
        NgayCt = entity.NgayCt;
        DoDay = entity.DoDay;
        KhoRong = entity.KhoRong;
        MauSac = entity.MauSac;
        TongCong = entity.TongCong;
        GhiChu = entity.GhiChu;
        ChungLoai = entity.ChungLoai;
    }

    public int Id { get; set; }

    public DateTime NgayCt { get; set; }

    public string DoDay { get; set; }

    public string KhoRong { get; set; }

    public string MauSac { get; set; }

    public double TongCong { get; set; }

    public string GhiChu { get; set; }

    public string ChungLoai { get; set; }

    public DateTime CreatedDate { get; set; }
}
