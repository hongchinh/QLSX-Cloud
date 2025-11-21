using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoolProof.Core;
using QLSX.Shared.Models;

namespace QLSX.Web.Data
{
    public class CustomerUpdate
    {

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

        [NotMapped]
        public string RegionName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Bạn phải chọn loại khách hàng")]
        public int CustomerTypeId { get; set; }

        [NotMapped]
        public string CustomerTypeName { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Bạn phải nhập vào trạng thái khách hàng")]
        public int StatusId { get; set; }

        [NotMapped]
        public string StatusName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Bạn phải nhập Nhân viên chăm sóc khách hàng")]
        public int EmployeeId { get; set; }

        [NotMapped]
        public string EmployeeName { get; set; }

        [RequiredIf("StatusId", Operator.EqualTo, 2, ErrorMessage = "Bạn phải nhập lý do dừng chăm sóc")]
        [DataType(DataType.MultilineText)]
        public string ReasonOfStop { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Bạn phải chọn sản phẩm")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

      
        public string Contents { get; set; }

        public DateTime DateNext { get; set; }

       
        public string ContentsNext { get; set; }
        [NotMapped]
        public virtual ICollection<CustomerCare> CustomerCares { get; set; }

        public DateTime CreateDate { get; set; }
        public string PictureUri { get; set; } = string.Empty;
        public string PictureBase64 { get; set; } = string.Empty;
        public string PictureName { get; set; } = string.Empty;
        public bool  IsFinished { get; set; }
    }
}
