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
            
            // Ưu tiên lấy TenantId từ JWT claims, fallback về header nếu không có
            var httpContext = accessor?.HttpContext;
            if (httpContext != null && httpContext.User != null)
            {
                var tenantIdClaim = httpContext.User.FindFirst("TenantId")?.Value;
                if (!string.IsNullOrEmpty(tenantIdClaim) && int.TryParse(tenantIdClaim, out int tenantIdFromClaim))
                {
                    TenantId = tenantIdFromClaim;
                }
                else
                {
                    // Fallback về header
                    StringValues tenantIdHeader = StringValues.Empty;
                    httpContext.Request?.Headers?.TryGetValue(QLSX.Shared.Contansts.RequestHeaders.XTenantId, out tenantIdHeader);
                    TenantId = int.Parse(string.IsNullOrEmpty(tenantIdHeader.ToString()) ? "0" : tenantIdHeader.ToString());
                }

                // Ưu tiên lấy UserId từ JWT claims, fallback về header nếu không có
                var userIdClaim = httpContext.User.FindFirst("UserId")?.Value;
                if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out int userIdFromClaim))
                {
                    UserId = userIdFromClaim;
                }
                else
                {
                    // Fallback về header
                    StringValues userIdHeader = StringValues.Empty;
                    httpContext.Request?.Headers?.TryGetValue(QLSX.Shared.Contansts.RequestHeaders.XUserId, out userIdHeader);
                    UserId = int.Parse(string.IsNullOrEmpty(userIdHeader.ToString()) ? "0" : userIdHeader.ToString());
                }
            }
            else
            {
                // Nếu không có HttpContext, lấy từ header
                StringValues tenantId = StringValues.Empty;
                accessor?.HttpContext?.Request?.Headers?.TryGetValue(QLSX.Shared.Contansts.RequestHeaders.XTenantId, out tenantId);
                TenantId = int.Parse(string.IsNullOrEmpty(tenantId.ToString()) ? "0" : tenantId.ToString());

                StringValues userId = StringValues.Empty;
                accessor?.HttpContext?.Request?.Headers?.TryGetValue(QLSX.Shared.Contansts.RequestHeaders.XUserId, out userId);
                UserId = int.Parse(string.IsNullOrEmpty(userId.ToString()) ? "0" : userId.ToString());
            }
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
                if (user == null)
                {
                    return new User();
                }
            }
            return user;
        }
    }
}
