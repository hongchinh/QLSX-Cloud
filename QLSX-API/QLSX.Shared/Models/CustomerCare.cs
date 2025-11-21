using FoolProof.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models 
{
    public class CustomerCare
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập nội dung chăm sóc")]
        public string Contents { get; set; }


        [Required(ErrorMessage = "Bạn phải nhập vào ngày chăm sóc")]
        public DateTime DateCreated { get; set; }


        public string DateCreatedST => this.DateCreated.ToString("dd/MM/yyyy");


        [RequiredIfFalse("IsFinished", ErrorMessage = "Bạn phải nhập nội dung chăm sóc.")]
        public DateTime DateNext { get; set; }

        public string DateNextST => this.DateNext.ToString("dd/MM/yyyy");
        public string Note { get; set; }

        [RequiredIfFalse("IsFinished", ErrorMessage = "Bạn phải nhập nội dung chăm sóc.")]
        public string ContentsNext { get; set; }

        public int ProductId { get; set; }
        public bool IsRead { get; set; }
        public string ProductName { get; set; }

        public double Price { get; set; }
        public bool IsFinished { get; set; }
        public bool IsStoped { get; set; }

        public virtual Customer Customer { get; set; }
    }

}
