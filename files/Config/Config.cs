using System;

namespace NetCoreWebGoat.Config
{
    public class AppConfig
    {
        private string _environmentName;

        public AppConfig()
        {
            _environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            DatabaseConnectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
        }

        public string DatabaseConnectionString { get; set; }

        public bool IsDevelopment => _environmentName == "Development";
    }
}