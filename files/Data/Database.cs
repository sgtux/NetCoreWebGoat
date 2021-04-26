using System;
using System.Threading;
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
                var tries = 10;
                Exception exception = null;
                while (tries > 0)
                {
                    try
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
                        tries = 0;
                        exception = null;
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                        tries--;
                        Thread.Sleep(5000);
                    }
                }
                if (tries == 0 && exception != null)
                    throw exception;
            }
        }
    }
}