using System;

namespace NetCoreWebGoat.Config
{
    public class AppConfig
    {
        private readonly string _environmentName;

        public readonly int CookieExpiresInMinutes;

        public AppConfig()
        {
            _environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            DatabaseConnectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
            CookieExpiresInMinutes = Convert.ToInt32(Environment.GetEnvironmentVariable("COOKIE_EXPIRES_IN_MINUTES"));
        }

        public string DatabaseConnectionString { get; set; }

        public bool IsDevelopment => _environmentName == "Development";
    }
}