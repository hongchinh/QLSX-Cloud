using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models.Request;

public class SearchNhatKisRequest : BaseRequest
{
    public string? ChucNang { get; set; }
    public string? Error { get; set; }
    public DateTime? ThoiGian_From { get; set; }
    public DateTime? ThoiGian_To { get; set; }
    public int? UserId { get; set; }
    public int? TenantId { get; set; }

    public int? IdPhieu { get; set; }

}
