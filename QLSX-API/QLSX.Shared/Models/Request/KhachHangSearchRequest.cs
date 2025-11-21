
using MudBlazor;
using QLSX.Shared.Entities;
using System;
using System.Collections.Generic;

namespace QLSX.Shared.Models
{
    public class KhachHangSearchRequest : BaseRequest
    {
        public string SearchText { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string DienThoai1 { get; set; }
        public string MaNhom { get; set; }
        public string MaTinh { get; set; }
        public ICollection<FilterDefinition<DanhMucKhachHang>> Filter { get; set; }
    }
}
