using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Web.Data
{
    public class TraCuuNhapXuatAll 
    {
        public int  DMDonViSuDungId { get; set; }
        public int Id { get; set; }
        public string Loai { get; set; }
        public DateTime  NgayCT { get; set; }
        public string  SoCT { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string DiaChi { get; set; }
        public string MaDonHang { get; set; }
        public Double  SoTien { get; set; }
        public Double SoTienTT { get; set; }
        public string DienGiai { get; set; }

    }

    public class ViewNhapXuat
    {
        public int Id { get; set; }
        public string Loai { get; set; }
        public DateTime NgayCT { get; set; }
        public string SoCT { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string DiaChi { get; set; }
        public Double SoTien { get; set; }
        public string DienGiai { get; set; }

    }
}
