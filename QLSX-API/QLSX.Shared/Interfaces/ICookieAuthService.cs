using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLSX.Shared.Interfaces
{
    public interface ICookieAuthService
    {
        Task CookieSignInAsync(HttpContext httpContext, IEnumerable<Claim> claims, bool rememberMe);

        Task CookieSignOutAsync(HttpContext httpContext);
    }
}
