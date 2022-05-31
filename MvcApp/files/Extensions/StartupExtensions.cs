using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetCoreWebGoat.Auth;
using NetCoreWebGoat.Config;

namespace NetCoreWebGoat.Extentions
{
    public static class StartupExtensions
    {
        public static void ConfigureAuth(this IServiceCollection services, AppConfig config)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.JwtKey));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "JWT_OR_COOKIE";
                options.DefaultChallengeScheme = "JWT_OR_COOKIE";
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.None;
                options.Cookie.HttpOnly = false;
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                options.Cookie.Name = ".NetCoreWebGoatCookie";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(config.CookieExpiresInMinutes);
                options.LoginPath = "/Account/Login";
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SecurityTokenValidators.Clear();
                options.SecurityTokenValidators.Add(new AppJwtTokenHandler());
                options.TokenValidationParameters = new AppTokenValidationParameters(secretKey);
            })
            .AddPolicyScheme("JWT_OR_COOKIE", "JWT_OR_COOKIE", options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

                    if (authHeader?.ToLower().StartsWith("bearer ") == true)
                        return AppTokenValidationParameters.AuthenticationScheme;

                    return CookieAuthenticationDefaults.AuthenticationScheme;
                };
            });
        }
    }
}