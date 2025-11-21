using QLSX.Shared.Enums;

namespace QLSX.Shared.Models;

public class BaseRequest : BaseModel
{
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public int Page { get; set; }

    public string SortLable { get; set; }
    public SortDirection SortDirection { get; set; }

    public double SumSoTien1 { get; set; }
    public double SumSoTien2 { get; set; }
}
