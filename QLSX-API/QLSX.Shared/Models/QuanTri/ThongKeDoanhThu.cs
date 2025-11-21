using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class ThongKeDoanhThu 
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string ColumnName { get; set; }
         
    }
    public class ThongKeDoanhThuTheoNV
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public double Value { get; set; }
        public string ColumnName { get; set; }

    }
    public class ThongKeLoaiTien
    {
        public string TenLoaiTien { get; set; }
        public double SoDuCuoi { get; set; }
    }

    public class TongHopDongTienModel
    {
        public int Id { get; set; }
        public int Stt { get; set; }
        public string TenLoaiTien { get; set; }
        public double SoDuDau { get; set; }
        public double SoTienThu { get; set; }
        public double SoTienChi { get; set; }
        public double SoDuCuoiKy { get; set; }
    }
}
