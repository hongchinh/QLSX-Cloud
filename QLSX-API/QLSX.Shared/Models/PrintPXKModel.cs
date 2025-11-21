using System;
using System.Collections.Generic;

namespace QLSX.Shared.Models;

public class PrintPXKModel
{
    public string MaDonVi { get; set; }

    public string TenDonVi { get; set; }

    public string SoPhieuXuat { get; set; }

    public DateTime? Ngay { get; set; }

    public TimeSpan? ThoiGian { get; set; }

    public double SoTienHang { get; set; }

    public double GiamCongNo { get; set; }

    public double PhiVanChuyen { get; set; }

    public double ChietKhau { get; set; }

    public double TyLeVat { get; set; }

    public double SoTienTT { get; set; }

    public string GhiChu { get; set; }

    public List<int> IdIdList { get; set; }

    public double TongThanhToan
    {
        get
        {
            return SoTienHang - GiamCongNo - ChietKhau + PhiVanChuyen + SoTienVat - SoTienTT;
        }
        set { }
    }

    public double SoTienVat
    {
        get
        {
            return SoTienHang * TyLeVat / 100;
        }
        set { }
    }
}

