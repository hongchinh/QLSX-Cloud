using CubeCloud.Common.Constants;
using System.Security.Claims;

namespace SaleAPI.Extensions
{
    public static class ApiExtensions
    {
        public static int DefaultPageIndex(this int value)
        {
            return (value <= 0) ? 1 : value;
        }

        public static int DefaultPageCount(this int value)
        {
            return (value <= 0) ? 10 : value;
        }

        public static string GetNameFromCookie(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static string GetNameIdentifierFromCookie(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetRoleFromCookie(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role)?.Value;
        }

        //public static string GetTenantIdFromCookie(this ClaimsPrincipal user)
        //{
        //    return user.FindFirst(GlobalConstants.TenantId)?.Value;
        //}

        public static string GetUniqueGuidFromCookie(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.SerialNumber)?.Value;
        }
    }
}
