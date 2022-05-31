using Microsoft.IdentityModel.Tokens;

namespace NetCoreWebGoat.Auth
{
    public class AppTokenValidationParameters : TokenValidationParameters
    {
        public const string AuthenticationScheme = "Bearer";

        public AppTokenValidationParameters(SymmetricSecurityKey secretKey)
        {
            ValidateLifetime = true;
            ValidateAudience = false;
            ValidateIssuer = false;
            ValidateIssuerSigningKey = false;
            IssuerSigningKey = secretKey;
        }
    }
}