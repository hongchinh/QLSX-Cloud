using System.Collections.Generic;

namespace QLSX.Shared.Models.Request;

public class SaveNoiDungDonHangTraNoRequest
{
    public List<NoiDungNhapXuatTraNoModel> NoiDungTraNoList { get; set; }

    public string SoPhieuXuatKho { get; set; }
}
