using QLSX.Shared.Entities;
using System;

namespace QLSX.Shared.Models;

public class NhapXuatThongTinModel
{
    public int Id { get; set; }

    public string? LoaiPhieu { get; set; }

    public double? SoTienGiam { get; set; }

    public double? SoTienCK { get; set; }

    public double? SoTienVc { get; set; }

    public double? TyLeVat { get; set; }

    public double? SoTienVat { get; set; }

    public double? SoTienTT { get; set; }

    public double? SoTien { get; set; }

    public DateTime? NgayCt { get; set; }

    public string? SoCt { get; set; }

    public double? TongCong { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? GhiChu { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public string? IdId { get; set; }

    public NhapXuatThongTinModel()
    {
    }

    public NhapXuatThongTinModel(NhapXuatThongTin entity)
    {
        Id = entity.Id;
        LoaiPhieu = entity.LoaiPhieu;
        SoTienGiam = entity.SoTienGiam;
        SoTienCK = entity.SoTienCK;
        SoTienVc = entity.SoTienVc;
        TyLeVat = entity.TyLeVat;
        SoTienVat = entity.SoTienVat;
        SoTienTT = entity.SoTienTT;
        SoTien = entity.SoTien;
        NgayCt = entity.NgayCt;
        SoCt = entity.SoCt;
        TongCong = entity.TongCong;
        CreatedDate = entity.CreatedDate;
        UpdatedDate = entity.UpdatedDate;
        GhiChu = entity.GhiChu;
        CreateBy = entity.CreateBy;
        UpdateBy = entity.UpdateBy;
        IdId = entity.IdId;
    }
}
