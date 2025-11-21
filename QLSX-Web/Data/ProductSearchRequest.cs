
using System;
namespace CRMApp.Data
{
    public class ProductSearchRequest : BaseRequest
    {
        public string MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public string DonViTinh { get; set; }


    }
}
