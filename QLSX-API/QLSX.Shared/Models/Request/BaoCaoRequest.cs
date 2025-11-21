using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class BaoCaoRequest : BaseRequest
    {

        public string  FileName { get; set; }
        public string Type { get; set; }
        public string Loai { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        
    }
}
