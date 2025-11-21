using QLSX.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class TraCuuCongNoRequest : BaseRequest
    {
        public int Hien { get; set; } = 0;
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string TempTable { get; set; } = "ZZZZTEMP";
        public int Loai { get; set; } = 0;
        public string? MaDonVi { get; set; }
        public string? TenDonVi { get; set; }
        public string? DiaChi { get; set; }
        public string? DienThoai { get; set; }

      


    }
}
