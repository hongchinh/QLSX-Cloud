using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp.Data
{
    public class NhapXuatsave 
    {
        public int Id { get; set; }
        public string Loai { get; set; }
        public DateTime NgayCT { get; set; }
        public String  SoCT { get; set; }
        public String  MaDonVi { get; set; }
        public String  TenDonVi { get; set; }
        public String DiaChi { get; set; }
        public String DienThoai { get; set; }
        public List<NoiDungNhapXuatSave> NoiDungNhapXuats { get; set; } = new List<NoiDungNhapXuatSave>();
    }


}
