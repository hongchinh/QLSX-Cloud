using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp.Data
{
    public class NoiDungNhapXuatSave
    {
        public int Id { get; set; }
        public string MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public string DonViTinh { get; set; }

        public double SoLuong { get; set; }
        public double DonGia { get; set; }
        public double  SoTien { get; set; }
        public double KhoRongTon { get; set; }
        public double ChieuDai { get; set; }
        public int   NhapXuatId { get; set; }
        public int MaHangHoaId { get; set; }

        public CRMShared.Models.DMHangHoa DMHangHoa { get; set; }
    }


}
