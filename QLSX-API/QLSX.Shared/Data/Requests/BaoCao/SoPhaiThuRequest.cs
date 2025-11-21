
using QLSX.Shared.Constants;
using QLSX.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QLSX.Shared.Data.Requests.BaoCao
{
    public class SoPhaiThuRequest : IApiWrapperRequest
    {
        public string RequestPath => Path.Combine(ControllerPath.BaoCao, ApiBaoCaoPath.SoPhaiThu);
        public bool IsValid => true;
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int DMDonViSuDungId { get; set; }
    }
}
