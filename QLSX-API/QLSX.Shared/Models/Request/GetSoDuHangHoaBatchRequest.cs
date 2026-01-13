using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class GetSoDuHangHoaBatchRequest : BaseRequest
    {
        public string MaKho { get; set; }
        public DateTime Ngay { get; set; }
        public List<string> MaHangHoas { get; set; } = new List<string>();
    }
}
