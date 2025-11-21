using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models 
{
    public class CustomerCareVM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Contents { get; set; }
        public double  Price { get; set; }
        public DateTime  DateCreated { get; set; }
        public DateTime DateNext { get; set; }
        public string Note { get; set; }

        public string ContentsNext { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public bool IsRead { get; set; }
        public bool IsFinished { get; set; }
        public virtual Customer Customer { get; set; }
    }

}
