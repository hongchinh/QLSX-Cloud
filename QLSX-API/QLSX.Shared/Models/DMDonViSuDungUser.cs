using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class DMDonViSuDungUser
    {
        public int Id { get; set; }
        public string TenDonVi { get; set; }
        public List<UserModel> Users { get; set; }
    }
}
