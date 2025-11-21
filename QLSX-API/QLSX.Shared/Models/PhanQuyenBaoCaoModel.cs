using QLSX.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class PhanQuyenBaoCaoModel
    {

        public PhanQuyenBaoCaoModel(PhanQuyenBaoCao entity)
        {
            if (entity != null)
            {
                Id = entity.Id;
                UserId = entity.UserId ?? 0;
                HoTen = entity.HoTen;
                Selected = entity.Selected ?? false;
                TenBaoCao = entity.TenBaoCao ;
                ReportFile = entity.ReportFiles ;
                Loai = entity.LoaiBaoCao ;
            }
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DMBaoCaoId { get; set; }
        public string ReportFile { get; set; }
        public string ExcelFile { get; set; }
        public string TenBaoCao { get; set; }
        public string MaSo { get; set; }
        public string Loai { get; set; }
        public string HoTen { get; set; }
        public bool Selected { get; set; }
    }
}
