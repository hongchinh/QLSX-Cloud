
using QLSX.Shared.Constants;
using QLSX.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QLSX.Shared.Data.Requests.NhapXuat
{
    public class InPhieuChiRequest : IApiWrapperRequest
    {
        public string RequestPath => Path.Combine(ControllerPath.ThuChi, ApiPath.InPhieuChi);
        public bool IsValid => true;
        public int DMDonViSuDungId { get; set; }
        public int Id { get; set; }
    }
}
