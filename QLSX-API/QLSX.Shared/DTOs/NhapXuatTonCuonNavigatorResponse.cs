using QLSX.Shared.Models;

namespace QLSX.Shared.DTOs;

public class NhapXuatTonCuonNavigatorResponse : NhapXuatTonCuonModel
{
    public int Total { get; set; }
    public int NextIndex { get; set; }
}
