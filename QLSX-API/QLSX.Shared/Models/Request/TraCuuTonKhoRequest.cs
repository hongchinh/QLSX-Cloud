using QLSX.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class TraCuuTonKhoRequest : BaseRequest
    {


        public int Hien { get; set; } = 0;
        public int DMDonViSuDungId { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string TempTable { get; set; } = "ZZZZTEMP";

        public int DMKhoHangId { get; set; } = 0;
        public string? MaHangHoa { get; set; }
        public string? TenHangHoa { get; set; }
        public string? DonViTinh { get; set; }

        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }

        public string SortLable { get; set; }
        public SortDirection SortDirection { get; set; }


    }
}
