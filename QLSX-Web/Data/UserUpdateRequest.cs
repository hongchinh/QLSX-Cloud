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
    public partial class UserUpdateRequest : UserModel
    {
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu cũ.")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string PasswordOld { get; set; }

        [NotMapped]
        public string PasswordOld_Save {
            get
            {
                return this.MatKhau;
            }
        }

    }
}
