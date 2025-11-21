using CubeCloud.Common.Constants;
using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaleAPI.Extensions
{
    public static class ObjectExtension
    {
        public static string RemoveHtmlXss(this string htmlIn, string baseUrl = null)
        {
            if (htmlIn == null) return null;
            var sanitizer = new HtmlSanitizer();
            sanitizer.RemovingAttribute += (s, e) =>
            {
                var _dataImage = new List<string> { "data:image/gif", "data:image/jpeg", "data:image/png", "data:image/jpg", "http://", "https://" };

                switch (e.Tag.TagName)
                {
                    case "IMG":
                        {
                            if (_dataImage.Any(x => e.Attribute.Value.StartsWith(x)))
                            {
                               // e.Reason = RemoveReason.NotAllowedAttribute;
                                e.Cancel = true;
                            }

                            break;
                        }
                }
            };
            return sanitizer.Sanitize(htmlIn, baseUrl);
        }

        public static string FromBlockReason(this int blockReason)
        {
            switch (blockReason)
            {
                //case BlockReasons.TenantUserBlackList:
                //    return "Tenant user setting blacklist";
                //case BlockReasons.TenantBlackList:
                //    return "Tenant setting blacklist";
                //case BlockReasons.AucBlackList:
                //    return "AUC blacklist";
                //case BlockReasons.TenantUserCategoryFilter:
                //    return "Tenant user category filter setting";
                //case BlockReasons.TenantCategoryFilter:
                //    return "Tenant category filter setting";
                default:
                    return "";
            }
        }

        public static int AsInteger(this object inputObject)
        {
            int result;
            if (Int32.TryParse(inputObject.AsString(), out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        public static long AsLong(this object inputObject)
        {
            long result;
            if (Int64.TryParse(inputObject.AsString(), out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        public static string AsString(this Object inputObject)
        {
            if (inputObject == null || inputObject == DBNull.Value) return string.Empty;
            return inputObject.ToString();
        }

        public static Guid AsGuid(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString)) return Guid.Empty;
            Guid result;
            try
            {
                Guid.TryParse(inputString, out result);
            }
            catch
            {
                return Guid.Empty;
            }
            return result;
        }
    }
}