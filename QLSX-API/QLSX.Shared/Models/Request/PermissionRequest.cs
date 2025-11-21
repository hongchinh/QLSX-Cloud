using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class PermissionRequest
    {
        public int Id { get; set; }
        public string PerName { get; set; }

        public int UserId { get; set; }
        public int UserName { get; set; }
       

        public string Type { get; set; }

    }
}
