using QLSX.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class QuyenSuDungModel
    {
        public QuyenSuDungModel(QuyenSuDung entity)
        {
            if (entity != null)
            {
                Id = entity.Id;
                UserId = entity.UserId ?? 0;
                MaSo = entity.MaSo;
                ChucNang = entity.ChucNang;
                HoTen = entity.HoTen;
                Selectted = entity.Selectted ?? false;
                Them = entity.Them ?? false;
                Sua = entity.Sua ?? false;
                Xoa = entity.Xoa ?? false;
                XemIn = entity.XemIn ?? false;
            }
        }
        public QuyenSuDungModel()
        {
        
        } 
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MaSo { get; set; }
        public string ChucNang { get; set; }
        public string HoTen { get; set; }
        public bool Selectted { get; set; }
        public bool Them { get; set; }
        public bool Sua { get; set; }
        public bool Xoa { get; set; }
        public bool XemIn { get; set; }
     
        [NotMapped]
        public bool IsLoaded { get; set; }

    }
}
