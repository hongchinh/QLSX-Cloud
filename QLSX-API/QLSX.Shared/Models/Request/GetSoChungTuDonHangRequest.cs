using System;
using System.Collections.Generic;

namespace QLSX.Shared.Models.Request;

public class GetSoChungTuDonHangRequest
{
    public List<int> IdIdList { get; set; }

    public string HoTen { get; set; }

    public double SoTien { get; set; }

    public DateTime NgayCT { get; set; }
}

