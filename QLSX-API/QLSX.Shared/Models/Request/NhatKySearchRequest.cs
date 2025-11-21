using System;
namespace QLSX.Shared.Models;

public class NhatKySearchRequest : BaseRequest
{
    public string? ChucNang { get; set; }
    public string? SoChungTu { get; set; }
    public DateTime? ThoiGian_From { get; set; }
    public DateTime? ThoiGian_To { get; set; }
    public int? UserId { get; set; }
    public int? TenantId { get; set; }
    public string? IdPhieu { get; set; }
    public string? HoTen { get; set; }
}
