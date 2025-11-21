using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{

    public class DMTinhGia : BaseModel
    {
        public int Id { get; set; }
        public string TenGia { get; set; }
        public string CongThuc { get; set; }
        public string GhiChu { get; set; }
    }
}
