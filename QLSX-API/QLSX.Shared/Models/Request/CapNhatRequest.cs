using QLSX.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class CapNhatRequest : BaseRequest
    {

        public int DMKhoHangId { get; set; }
        [Required]
        public DateTime? TuNgay { get; set; }
        [Required]
        public DateTime? DenNgay { get; set; }


    }
}
