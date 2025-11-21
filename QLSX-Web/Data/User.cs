using FoolProof.Core;
using Microsoft.AspNetCore.Mvc;
using QLSX.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Web.Data
{
    public partial class User_BK
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập nội dung chăm sóc")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập mật khẩu.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập lại mật khẩu.")]
        [RequiredIfFalse("IsMatch", ErrorMessage = "Mật khẩu không giống nhau.")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }

        [NotMapped]
        public bool IsMatch
        {
            get
            {
                return (this.Password == this.RePassword);
            }
        }
        public bool  IsActive { get; set; }
        public string Source { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập họ và tên.")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }        
        
        public int id_kt { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Bạn phải nhập vào chức vụ")]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        
        public DateTime? HireDate { get; set; }

        public Role Role { get; set; }
        public QLSX.Shared.Models.DMPhongBan DMPhongBans { get; set; }

        [RequiredIfFalse("IsMatch", ErrorMessage = "Mật khẩu không giống nhau.")]
        public string ConfirmPassword { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Bạn phải nhập vào phòng ban")]
        public int DMPhongBanId { get; set; }
        public int DMDonViSuDungId { get; set; }

        [NotMapped]
        public bool IsAdmin
        {
            get
            {
                return (this.RoleId == 1);
            }
        }
    }
}
