using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class DMPhongBan : BaseModel
    {
        public int Id { get; set; }
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string DanhSachChucVu { get; set; }

    }
}
