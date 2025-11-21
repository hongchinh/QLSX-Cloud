using QLSX.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class ViewCongNoRequest : BaseRequest
    {
     
        public string Loai { get; set; }
        public int sodu { get; set; }
        public string? TuNgay { get; set; }
        public string? DenNgay { get; set; }
        public string TempTable { get; set; } = "ZZZZTEMP";
        public string? MaDonVi { get; set; }

    }
}
