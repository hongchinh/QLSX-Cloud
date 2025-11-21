using QLSX.Shared.Models;

namespace QLSX.Shared.DTOs;

public class ThuChiNavigatorResponse : ThuChiModel
{
    public int Total { get; set; }
    public int NextIndex { get; set; }
}
