using QLSX.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class GetAllResponse<T> :  BaseRequest
    {
        public List<T> Items;
        public List<T> ListData;
        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
        
}
