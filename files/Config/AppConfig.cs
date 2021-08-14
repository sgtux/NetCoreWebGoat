using System;
using NetCoreWebGoat.Models;

namespace NetCoreWebGoat.Config
{
    public class AppConfig
    {
        private readonly string _environmentName;

        public int CookieExpiresInMinutes { get; }

        public string DatabaseConnectionString { get; }

        public bool IsDevelopment => _environmentName == "Development";

        public AppConfig()
        {
            _environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            DatabaseConnectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
            CookieExpiresInMinutes = Convert.ToInt32(Environment.GetEnvironmentVariable("COOKIE_EXPIRES_IN_MINUTES"));
            CspHttpHeader = Environment.GetEnvironmentVariable("CSP_HTTP_HEADER");
        }

        public string CspHttpHeader { get; set; }

        public void Update(AppConfigModel model)
        {
            CspHttpHeader = model.CspHttpHeader;
        }
    }
}