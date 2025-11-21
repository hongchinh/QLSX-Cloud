
using QLSX.Shared.Constants;
using QLSX.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QLSX.Shared.Data.Requests.NhapXuat
{
    public class InPhieuXuatRequest : IApiWrapperRequest
    {
        public string RequestPath => Path.Combine(ControllerPath.NhapXuat, ApiPath.InPhieuXuat);
        public bool IsValid => true;
        public int DMDonViSuDungId { get; set; }
        public int Id { get; set; }
    }
}
