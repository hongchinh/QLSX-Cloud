using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class Customer
    {
        public Customer()
        {
            CustomerCares = new HashSet<CustomerCare>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAge { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Mobile3 { get; set; }
        public string Mobile4 { get; set; }
        public int RegionId { get; set; }
        public int CustomerTypeId { get; set; }
        public int StatusId { get; set; }
        public int EmployeeId { get; set; }
        [NotMapped]
        public string EmployeeName { get; set; } = string.Empty;
        public string ReasonOfStop { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public string Contents { get; set; }
        public DateTime  DateNext { get; set; }
        public string ContentsNext { get; set; }

        public virtual ICollection<CustomerCare> CustomerCares { get; set; }

        public string PictureUri { get; set; } = string.Empty;

        [NotMapped]
        public string PictureBase64 { get; set; } = string.Empty;
        public string PictureName { get; set; } = string.Empty;

        public void UpdatePictureUri(string pictureName)
        {
            if (string.IsNullOrEmpty(pictureName))
            {
                PictureUri = string.Empty;
                return;
            }
            PictureUri = $"images\\customers\\{pictureName}?{new DateTime().Ticks}";
        }
        public bool  IsFinished { get; set; } = false ;
        public DateTime DateFinished { get; set; } = DateTime .Now ;
        [NotMapped]
        public bool IsExitst { get; set; } = false;

    }
}
