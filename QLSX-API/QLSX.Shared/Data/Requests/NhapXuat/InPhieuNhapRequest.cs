
using QLSX.Shared.Constants;
using QLSX.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QLSX.Shared.Data.Requests.NhapXuat
{
    public class InPhieuNhapRequest : IApiWrapperRequest
    {
        public string RequestPath => Path.Combine(ControllerPath.NhapXuat, ApiPath.InPhieuNhap);
        public bool IsValid => true;
        public int DMDonViSuDungId { get; set; }
        public int Id { get; set; }
    }
}
