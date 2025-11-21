using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ReportAPINet.Models
{
    public static class TokenManager
    {

        public static bool ValidAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                string SecretKey = System.Configuration.ConfigurationManager.AppSettings.Get("SecretKey");
                string IssuerKey = System.Configuration.ConfigurationManager.AppSettings.Get("IssuerKey");
                string AudienceKey = System.Configuration.ConfigurationManager.AppSettings.Get("AudienceKey");
                var key = Encoding.ASCII.GetBytes(SecretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);
                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {

                    var userId = principle.FindFirst(ClaimTypes.Name)?.Value;
                    if (Convert.ToInt32(userId) > 0) return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}