using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class DanhMucKhoanThuModelChi : BaseModel
    {
        public int Id { get; set; }
        public string MaKhoan { get; set; }
        public string TenKhoan { get; set; }
        public string GhiChu { get; set; }
        
    }
}
