using System;
using System.Collections.Generic;

namespace QLSX.Shared.Models.Request;

public class UpdateTrangThaiRequest
{
    public int TrangThai { get; set; }
    public List<int> IdIdList { get; set; }
    public DateTime Ngay { get; set; }
    public string Spx { get; set; }
}
