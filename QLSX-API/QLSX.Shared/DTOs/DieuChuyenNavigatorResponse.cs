using QLSX.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.DTOs
{
   public class DieuChuyenNavigatorResponse : DieuChuyen
    {
        public int Total { get; set; }
        public int NextIndex { get; set; }
    }
}
