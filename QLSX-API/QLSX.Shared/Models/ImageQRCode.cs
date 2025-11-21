using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class ImageQRCode : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Loai { get; set; }
        public int IdPhieu { get; set; }
        
        public byte[] Bytes { get; set; }
        public string GhiChu { get; set; }
        public string FileExtension { get; set; }
        public double SoTien { get; set; }
        public double Size { get; set; }
        public int Width { get; set; }
        public int Hieght { get; set; }
       
    }
}
