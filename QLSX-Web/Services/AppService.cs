using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QLSX.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using QLSX.Web;

namespace QLSX.Web.Services
{
     
    public class AppService  
    {
        public int DMDonViSuDungId { get; set; }
        public int UserId { get; set; }
    }
}
