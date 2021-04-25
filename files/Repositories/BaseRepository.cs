using System;
using System.Collections.Generic;
using System.Data;
using NetCoreWebGoat.Config;
using Npgsql;

namespace NetCoreWebGoat.Repositories
{
    public abstract class BaseRepository
    {
        private readonly NpgsqlConnection _connection;

        public BaseRepository(AppConfig appConfig)
        {
            _connection = new NpgsqlConnection(appConfig.DatabaseConnectionString);
        }

        public NpgsqlDataReader Query(string sql)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            var command = _connection.CreateCommand();
            command.CommandText = sql;
            return command.ExecuteReader();
        }

        public void ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            var command = _connection.CreateCommand();
            command.CommandText = sql;
            if (parameters != null)
                foreach (var key in parameters.Keys)
                    command.Parameters.AddWithValue(key, parameters[key] ?? DBNull.Value);
            command.ExecuteNonQuery();
        }

        public T GetValueOrNull<T>(NpgsqlDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            if (columnValue is DBNull)
                return default(T);
            return (T)columnValue;
        }

        public void Dispose()
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }
    }
}