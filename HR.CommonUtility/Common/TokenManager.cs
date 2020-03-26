using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR.CommonUtility
{
    public class TokenManager
    {
        private static string strSecret = "MEQO3WI7JK2VNoaDvbncja/ZkqPLMNB30c+aR4yHzygn5qNBVcvbtBpw4+SwZh4+NBVCXi3KJHlSXKPri6bXr8==";
        private static string strIssuer = "IssuedBy";
        private static string strAudience = "Audience";
        private static Int16 intMinutes = 30;

        public static string GenerateToken(string strUserCode = "Default")
        {
            Guid gGuid = Guid.NewGuid();
            var vClaims = new[] {
                new Claim(ClaimTypes.GivenName, gGuid.ToString())
                //new Claim(ClaimTypes.Name, strUserCode),
                //new Claim(ClaimTypes.Sid, strAppId)
            };

            SymmetricSecurityKey sskSecurityKey = new SymmetricSecurityKey(Convert.FromBase64String(strSecret));
            SecurityTokenDescriptor stdDescriptor = new SecurityTokenDescriptor
            {
                Issuer = strIssuer,
                Audience = strAudience,
                Subject = new ClaimsIdentity(vClaims),
                Expires = DateTime.UtcNow.AddMinutes(intMinutes),
                SigningCredentials = new SigningCredentials(sskSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler jsthHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jstToken = jsthHandler.CreateJwtSecurityToken(stdDescriptor);
            return jsthHandler.WriteToken(jstToken);
        }

        public static ClaimsPrincipal GetPrincipal(string strToken)
        {
            try
            {
                JwtSecurityTokenHandler jsthHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jstToken = (JwtSecurityToken)jsthHandler.ReadToken(strToken);
                if (jstToken == null) return null;
                TokenValidationParameters tvpParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    //ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(strSecret))
                };
                SecurityToken stSecurityToken;
                ClaimsPrincipal cpPrincipal = jsthHandler.ValidateToken(strToken, tvpParameters, out stSecurityToken);
                return cpPrincipal;
            }
            catch
            {
                return null;
            }
        }

        public static string ValidateToken(string strToken)
        {
            ClaimsPrincipal cpPrincipal = GetPrincipal(strToken);
            if (cpPrincipal == null) return null;
            ClaimsIdentity ciIdentity = null;
            try
            {
                ciIdentity = (ClaimsIdentity)cpPrincipal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            Claim cUserClaim = ciIdentity.FindFirst(ClaimTypes.Name);
            return Convert.ToString(cUserClaim.Value);
        }
    }
}
