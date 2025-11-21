using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class GetSoDuCongNoRequest : BaseRequest
    {
        public string MaKhachHang { get; set; }
        
        public DateTime? Ngay { get; set; }
        public string Loai { get; set; }
        public string lPhieu { get; set; }
        public int Id { get; set; }
    }
}
