using QLSX.Shared.Models;

namespace QLSX.Shared.DTOs;

public class NhapXuatNavigatorResponse : NhapXuatModel
{
    public int Total { get; set; }
    public int NextIndex { get; set; }
}
