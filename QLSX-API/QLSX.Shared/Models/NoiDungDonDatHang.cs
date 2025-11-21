using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class NoiDungDonDatHang : BaseModel
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        [NotMapped] public int Index { get; set; }
        public int Id { get; set; }

        public string MaHangHoa { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập vào tên hàng hóa")]
        public string TenHangHoa { get; set; }
        public string DonViTinh { get; set; }

        public double SoLuong { get; set; }
        
        public double DonGia { get; set; }
        
        public double  SoTien { get; set; }

        public double DonGiaHoaHong { get; set; }

        public double SoTienHoaHong { get; set; }

        public int   DonDatHangId { get; set; }

        
        public double KhoRongTon { get; set; }
        
        public double ChieuDai { get; set; }

        
        public double TongChieuDai { get; set; }

        //[DisplayFormat(DataFormatString = "{0:###,###.##", ApplyFormatInEditMode = true)]
        public double TongDienTich { get; set; }
        public QLSX.Shared.Models.DanhMucHangHoaModel DMHangHoa { get; set; }
        public int DMHangHoaId { get; set; }
        public int DMKhoHangId { get; set; }
        public int DMTinhGiaId { get; set; }

        public bool IsEditing { get; set; }

        public string GhiChu { get; set; }

        [NotMapped]
        public bool IsM2 { get; set; }
        [NotMapped]
        public bool IsMD { get; set; }
        [NotMapped]
        public bool IsSoLuong { get; set; }
        [NotMapped]
        public double SoLuongTon { get; set; }



    }


}
