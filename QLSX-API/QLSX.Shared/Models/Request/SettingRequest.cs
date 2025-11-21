
using System;
namespace QLSX.Shared.Models
{
    public class SettingRequest : BaseRequest
    {
        public int UserId { get; set; }
        public string Keywords { get; set; }
    }
}
