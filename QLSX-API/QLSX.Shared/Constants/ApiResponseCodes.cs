using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Constants
{
    public static class ApiResponseCodes
    {
        public const int Success = 1;
        public const int OK = 200;
        public const int NotFound = 404;
        public const int TenantNotFound = 704;
        public const int BadRequest = 400;
        public const int TenantExpired = 600;
        public const int TenantDisable = 700;
        public const int TenantNotHaveContractBasic = 701;
        public const int TenantNotStartDateContractBasic = 702;
        public const int TenantExpire = 703;
        public const int FSecureinvalidToken = 401;
        public const int Exception = 409;
        public const int FSecureDelete = 204;
    }
}
