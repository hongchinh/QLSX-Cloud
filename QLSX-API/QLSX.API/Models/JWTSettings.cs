using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleAPI.Models
{
    public class JWTSettings
    {
        public string SecretKey { get; set; }
        public string IssuerKey { get; set; }
        public string AudienceKey { get; set; }
    }
}
