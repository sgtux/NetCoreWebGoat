using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace NetCoreWebGoat.Auth
{
    public class AppJwtTokenHandler : JwtSecurityTokenHandler
    {
        public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters parameters, out SecurityToken validatedToken)
        {
            try
            {
                var jwt = new JwtSecurityToken(token);
                parameters.RequireSignedTokens = jwt.SignatureAlgorithm != SecurityAlgorithms.None;
            }
            catch { }
            return base.ValidateToken(token, parameters, out validatedToken);
        }
    }
}