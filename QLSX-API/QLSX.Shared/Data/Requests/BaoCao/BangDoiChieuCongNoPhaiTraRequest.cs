
using QLSX.Shared.Constants;
using QLSX.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QLSX.Shared.Data.Requests.BaoCao
{
    public class BangDoiChieuCongNoPhaiTraRequest : IApiWrapperRequest
    {
        public string RequestPath => Path.Combine(ControllerPath.BaoCao, ApiBaoCaoPath.BangDoiChieuCongNoPhaiTra);
        public bool IsValid => true;
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int DMDonViSuDungId { get; set; }
    }
}
