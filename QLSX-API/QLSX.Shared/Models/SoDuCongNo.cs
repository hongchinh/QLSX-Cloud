using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class SoDuCongNo : BaseModel
    {
        public int Id { get; set; }
        public int DMKhachHangId { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string? DiaChi { get; set; }
        public string? DienThoai { get; set; }
        public double SoTien { get; set; }
        public string Loai { get; set; }  /*PHAITHU - PHAITRA*/
        public string? GhiChu { get; set; }  /*PHAITHU - PHAITRA*/

        
    }
}
