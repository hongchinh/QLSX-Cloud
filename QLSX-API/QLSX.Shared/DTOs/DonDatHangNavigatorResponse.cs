using QLSX.Shared.Models;

namespace QLSX.Shared.DTOs;

public class DonDatHangNavigatorResponse : DonDatHangModel
{
    public int Total { get; set; }
    public int NextIndex { get; set; }
}
