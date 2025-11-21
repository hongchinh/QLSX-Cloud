using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class NoiDungDieuChuyen : BaseModel
    {
        public int Id { get; set; }

        public string MaHangHoa { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập vào tên hàng hóa")]
        public string TenHangHoa { get; set; }
        public string DonViTinh { get; set; }

        public double SoLuong { get; set; }
        
        public double DonGia { get; set; }
        
        public double  SoTien { get; set; }

        public int   DieuChuyenId { get; set; }

        
        public double KhoRongTon { get; set; }
        
        public double ChieuDai { get; set; }

        
        public double TongChieuDai { get; set; }

        //[DisplayFormat(DataFormatString = "{0:###,###.##", ApplyFormatInEditMode = true)]
        public double TongDienTich { get; set; }
        public QLSX.Shared.Models.DanhMucHangHoaModel DMHangHoa { get; set; }
        public int DMHangHoaId { get; set; }
        public int DMKhoHangId { get; set; }

        public string? GhiChu { get; set; }
    }


}
