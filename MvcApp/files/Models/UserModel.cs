using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json.Serialization;
using NetCoreWebGoat.Helpers;
using Npgsql;

namespace NetCoreWebGoat.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Photo { get; set; }

        public int Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public UserModel() { }

        public UserModel(NpgsqlDataReader dataReader) => FillFromDataReader(dataReader);

        public void FillFromDataReader(NpgsqlDataReader dataReader)
        {
            Id = DatabaseHelper.GetValueOrNull<int>(dataReader, "Id");
            Name = DatabaseHelper.GetValueOrNull<string>(dataReader, "Name");
            LastName = DatabaseHelper.GetValueOrNull<string>(dataReader, "LastName");
            Email = DatabaseHelper.GetValueOrNull<string>(dataReader, "Email");
            Password = DatabaseHelper.GetValueOrNull<string>(dataReader, "Password");
            Photo = DatabaseHelper.GetValueOrNull<string>(dataReader, "Photo");
            Role = DatabaseHelper.GetValueOrNull<int>(dataReader, "Role");
            CreatedAt = DatabaseHelper.GetValueOrNull<DateTime>(dataReader, "CreatedAt");
            UpdatedAt = DatabaseHelper.GetValueOrNull<DateTime?>(dataReader, "UpdatedAt");
        }

        [JsonIgnore]
        public List<Claim> Claims
        {
            get
            {
                return new List<Claim>
                {
                    new Claim("Id", Id.ToString()),
                    new Claim(ClaimTypes.Name, Name),
                    new Claim(ClaimTypes.Role, Role.ToString()),
                    new Claim("Photo", Photo ?? "")
                };
            }
        }
    }
}