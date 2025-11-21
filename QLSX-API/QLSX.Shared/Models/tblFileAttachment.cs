using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class tblFileAttachment : BaseModel
    {
        /// <summary>
        /// File Directory
        /// </summary>
        public int id { get; set; }
        public string FileName { get; set; }
        public string FileExtention { get; set; }
        public DateTime CreateDate { get; set; }
        public string Loai { get; set; }
        public int IdPhieu { get; set; }
        public int UserId { get; set; }

        /// <summary>
        /// File Attachment
        /// </summary>
        public byte[] FileBytes { get; set; }
    }
}
