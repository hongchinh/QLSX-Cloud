using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class GetSoDuHangHoaRequest : BaseRequest
    {
        public string MaHangHoa { get; set; }
        public string MaKho { get; set; }
        public DateTime Ngay { get; set; }
    }
}
