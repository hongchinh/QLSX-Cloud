using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Web.Data
{
    public class GetAllResponsePaged_BAK<T> : BaseRequest
    {
        public List<T> Items;
        public List<T> ListData { get; set; }
        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}
