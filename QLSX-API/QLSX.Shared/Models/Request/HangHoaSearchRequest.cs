
using MudBlazor;
using System;
using System.Collections.Generic;

namespace QLSX.Shared.Models
{
    public class HangHoaSearchRequest : BaseRequest
    {
        public string SearchText { get; set; }
        public string MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public string DonViTinh { get; set; }
        public string MaNhom { get; set; }
        public string MaMauSac { get; set; }
        public string MaDoDay { get; set; }
        public string MaLoaiTon { get; set; }
        public string MaChungLoai { get; set; }
       
        public string MaKieuSong { get; set; }
        
    }
}
