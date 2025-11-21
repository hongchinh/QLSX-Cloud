using QLSX.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Web.Data
{
    public class BaseRequest
    {

        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Page { get; set; }

        public string SortLable { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
