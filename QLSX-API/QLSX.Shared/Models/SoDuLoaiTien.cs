using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class SoDuLoaiTien : BaseModel
    {
        public int Id { get; set; }
        public int DMLoaiTienId { get; set; }
        public string KyHieu { get; set; }
        public string TenLoaiTien { get; set; }
        public double SoTien { get; set; }

       
    }
}
