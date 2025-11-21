using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class BaoCao : BaseModel
    {
        public int Id { get; set; }
        public int OrderById { get; set; }
        public string Loai { get; set; }
        public string MaSo { get; set; }
        public string ReportFiles { get; set; }
        public string ExcelFiles { get; set; }
        public string TenBaoCao { get; set; }
        public bool Selected { get; set; }
     
    }
}
