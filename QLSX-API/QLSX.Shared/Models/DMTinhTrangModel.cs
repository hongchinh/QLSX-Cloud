using QLSX.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class DMTinhTrangModel  
    {
        public DMTinhTrangModel(DMTinhTrang entity)
        {
            if (entity != null)
            {
                Id = entity.Id;
                Stt = entity.Stt ?? 0;
                TenTrangThai = entity.TenTrangThai;
                GhiChu = entity.GhiChu;
            }
        }

        public DMTinhTrangModel()
        {
        }

        public int Id { get; set; }
        public int Stt { get; set; }
        public string TenTrangThai { get; set; }
        public string GhiChu { get; set; }
         
    }
}
