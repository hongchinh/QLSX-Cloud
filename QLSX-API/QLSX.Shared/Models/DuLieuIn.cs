using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class DuLieuIn : BaseModel
    {
        public Guid Id { get; set; }
        public int IdMaSo { get; set; }
        public string Loai { get; set; }
        public int UserId { get; set; }
        
    }
}
