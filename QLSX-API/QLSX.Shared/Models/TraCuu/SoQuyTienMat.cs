using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class SoQuyTienMat
    {
        public string SoPhieu { get; set; }
        public int Stt { get; set; }
        public DateTime NgayCT { get; set; }
        public string DienGiai { get; set; }

       
         
        public Double SoTienThu { get; set; }
        public Double SotienChi { get; set; }
        public Double SoTienTon { get; set; }
        public Double SoDauKy { get; set; }
        public Double SoDuCuoi { get; set; }
        public int DMDonViSuDungId { get; set; }
        public string TenDonViSuDung { get; set; }
              
       
        public int DMLoaiTienId { get; set; }
        public string TenLoaiTien { get; set; }
        public string ThoiGian { get; set; }
        
        public int Id { get; set; }

    }


}
