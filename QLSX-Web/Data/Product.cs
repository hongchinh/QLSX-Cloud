using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Web.Data
{
    public class Product_BK
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập vào tên sản phẩm")]
        public string ProductName { get; set; }
        public string Note { get; set; }
        public double Price { get; set; }
      
    }

}
