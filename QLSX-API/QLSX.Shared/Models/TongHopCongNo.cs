using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class TongHopCongNo
    {
         
        
        public int Id { get; set; }
        public string MDonVi { get; set; }
        public string MNhom { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string TenDonVi { get; set; }
        public decimal  SoDauKy { get; set; }
        public decimal SoTienMua { get; set; }
        public decimal SoTienTra { get; set; }
        public decimal SoCuoiKy { get; set; }
    }
}
