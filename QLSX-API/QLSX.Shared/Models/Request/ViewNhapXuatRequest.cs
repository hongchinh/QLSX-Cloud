using QLSX.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class ViewNhapXuatRequest : BaseRequest
    {
     
        public string Loai { get; set; }
        public string? TuNgay { get; set; }
        public string? DenNgay { get; set; }
        public string TempTable { get; set; } = "ZZZZTEMP";

        public string MaKho { get; set; } = "";
        public int DMHangHoaId { get; set; } = 0;
        public string? MaHangHoa { get; set; }
        public string? TenHangHoa { get; set; }
        public string? DonViTinh { get; set; }

    }
}
