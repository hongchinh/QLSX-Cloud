using SaleAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using QLSX.Shared.Models;
using User = QLSX.Shared.Entities.User;

namespace SaleAPI.Interfaces
{
    public interface ITenantProvider
    {
        int TenantId { get; set; }
        int UserId { get; set; }

        QLSX.Shared.Models.DMDonViSuDung GetTenant(bool ignoreStatus = false);
         User GetUser(bool ignoreStatus = false);
    }

    public class TenantProvider : ITenantProvider
    {
        private QLSX.Shared.Models.DMDonViSuDung tenant;
        private User user;

        private readonly CRMDBContext tenantContext;

        public int TenantId { get; set; }
        public int UserId { get; set; }

        public TenantProvider(IHttpContextAccessor accessor, CRMDBContext context)
        {
            tenantContext = context;
            StringValues  tenantId  = StringValues.Empty;
            accessor?.HttpContext?.Request?.Headers?.TryGetValue(QLSX.Shared.Contansts.RequestHeaders.XTenantId, out tenantId);
            TenantId = int.Parse( string.IsNullOrEmpty(tenantId.ToString()) ? "0" : tenantId.ToString());

            accessor?.HttpContext?.Request?.Headers?.TryGetValue(QLSX.Shared.Contansts.RequestHeaders.XUserId, out tenantId);
            UserId = int.Parse(string.IsNullOrEmpty(tenantId.ToString()) ? "0" : tenantId.ToString());


        }

        public DMDonViSuDung GetTenant(bool ignoreStatus = false)
        {
            if ( TenantId == 0)
            {
                return new DMDonViSuDung();
            }
            else
            {
                tenant = tenantContext.GetByTenantId(TenantId, ignoreStatus);
                if (tenant == null)
                {
                    return new DMDonViSuDung();
                }
            }
            return tenant;
        }

        public User GetUser(bool ignoreStatus = false)
        {
            if (UserId == 0)
            {
                return new User();
            }
            else
            {
                user = tenantContext.GeUserById(UserId, ignoreStatus);
                if (tenant == null)
                {
                    return new User();
                }
            }
            return user;
        }
    }
}
