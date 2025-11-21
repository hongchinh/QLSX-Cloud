using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class SearchSoDuHangHoaRequest : BaseRequest
    {
        public int DMKhoHangId { get; set; }
        public string MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public string DonViTinh { get; set; }
    }
}
