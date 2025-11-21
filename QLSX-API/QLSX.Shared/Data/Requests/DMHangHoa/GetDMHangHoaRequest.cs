
using QLSX.Shared.Constants;
using QLSX.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QLSX.Shared.Data.Requests.DMHangHoa
{
    public class NhapXuatRequest : IApiWrapperRequest
    {
        public string RequestPath => Path.Combine(ControllerPath.DMHangHoa, ApiPath.All);
        public bool IsValid => true;
        public int DMDonViSuDungId { get; set; }
    }
}
