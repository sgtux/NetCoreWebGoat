using System;
using NetCoreWebGoat.Config;
using Npgsql;

namespace NetCoreWebGoat.Data
{
    public class Database
    {
        private readonly AppConfig _appConfig;
        public Database(AppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public void Initialize()
        {
            using (var connection = new NpgsqlConnection(_appConfig.DatabaseConnectionString))
            {
                connection.Open();
                bool runScript;

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(1) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'User'";
                    runScript = Convert.ToInt32(command.ExecuteScalar()) == 0;
                }

                if (runScript)
                {
                    var sql = System.IO.File.ReadAllText("./script.sql");
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}