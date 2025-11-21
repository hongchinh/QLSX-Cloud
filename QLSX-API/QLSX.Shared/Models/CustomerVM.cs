using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class CustomerVM : Customer
    {
        public virtual int Stt { get; set; }
        public virtual string RegionName { get; set; }
        public virtual string ProductName { get; set; }
        public virtual string CustomerTypeName { get; set; }
        public virtual string StatusName { get; set; }
        public virtual string EmployeeName { get; set; }
        public virtual string TinhTrang { get; set; }
        public virtual string ChamSoc { get; set; }
        public virtual ICollection<DonHang> lstDonHang { get; set; }
    }
}
