using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared
{
    public class Contansts
    {
        public const string AUTH_KEY = "AuthKeyOfDoomThatMustBeAMinimumNumberOfBytes";
        public static class RequestHeaders
        {
            public const string XTenantId = "X-Tenant-Id";
            public const string XUserId = "X-User-Id";
        }

        public const string TenantId = "TenantId";
        public const string UserId = "UserId";
    }
    
}
