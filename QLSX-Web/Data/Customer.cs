using Microsoft.AspNetCore.Mvc;
using QLSX.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Web.Data
{
    public class Customer_BK
    {
        public int Stt { get; set; }
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập vào tên khách hàng")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập vào số điện thoại")]
         public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Mobile3 { get; set; }
        public string Mobile4 { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Bạn phải nhập vào khu vực của khách hàng")]
        public int RegionId { get; set; }
        public string RegionName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Bạn phải chọn loại khách hàng")]
        public int CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Bạn phải nhập vào trạng thái khách hàng")]
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Bạn phải nhập Nhân viên chăm sóc khách hàng")]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public string ReasonOfStop { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Bạn phải chọn sản phẩm")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

       
        public string Contents { get; set; }

        
        public DateTime DateNext { get; set; }

       
        public string ContentsNext { get; set; }

        [NotMapped]
        public string TinhTrang { get; set; }
       
        [NotMapped]
        public string ChamSoc { get; set; }

        [NotMapped]
        public bool IsNotCS
        {
            get
            {
                if (this.TinhTrang != null)
                {
                    if(this.TinhTrang.Length >0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<CustomerCare> CustomerCares { get; set; }

        public string PictureUri { get; set; } = string.Empty;
        public string PictureBase64 { get; set; } = string.Empty;
        public string PictureName { get; set; } = string.Empty;

        public bool IsFinished { get; set; }

        [NotMapped]
        public virtual ICollection<DonHang> lstDonHang { get; set; }

        [NotMapped]
        public bool IsExitst { get; set; }
    }
}
