using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class SearchSoDuCongNoRequest : BaseRequest
    {
        public int DMKhachHangId { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string DiaChi { get; set; }
        public string Loai { get; set; }
        public string DienThoai { get; set; }
    }
}
