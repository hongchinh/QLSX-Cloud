
using MudBlazor;
using QLSX.Shared.Entities;
using System.Collections.Generic;

namespace QLSX.Shared.Models
{
    public class HangHoaTonCuonSearchRequest : BaseRequest
    {
        
        public string SearchText { get; set; }
        public string MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public string DonViTinh { get; set; }
        public int DMNhomHangId { get; set; }
        public int DMMauSacId { get; set; }
        public int DMDoDayId { get; set; }
        public int DMLoaiTonId { get; set; }
        public int DMChungLoaiId { get; set; }
        public int DMKieuSongId { get; set; }
        public ICollection<FilterDefinition<DanhMucHangHoaTonCuon>> Filter { get; set; }
    }
}
