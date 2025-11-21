using System.Collections.Generic;

namespace QLSX.Shared.Models;

public class GetDonHangResponse<T>
{
    public List<T> Items { get; set; }

    public int TotalRows { get; set; }

    public double TotalMd { get; set; }

    public double TotalM2 { get; set; }

    public double TotalSoLuong { get; set; }

    public double TotalSoTien { get; set; }
}
