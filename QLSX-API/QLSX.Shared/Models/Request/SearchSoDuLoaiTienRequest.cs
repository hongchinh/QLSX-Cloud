using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class SearchSoDuLoaiTienRequest : BaseRequest
    {
       
        public string KyHieu { get; set; }
        public string TenLoaiTien { get; set; }
       
    }
}
