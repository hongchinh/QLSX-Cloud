using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Web.Data
{
    public class TongHopCongNo_BK
    {
         
        
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string MNhom { get; set; }
        public decimal  SoDauKy { get; set; }
        public decimal SoTienMua { get; set; }
        public decimal SoTienTra { get; set; }
        public decimal SoCuoiKy { get; set; }
    }
}
