using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp.Data
{
    public class RegionSaleVM
    {
        public int Id { get; set; }
        public string TenDonVi { get; set; }
        public List<User> Users { get; set; }
    }
}
