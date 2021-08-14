using System;
using NetCoreWebGoat.Helpers;
using Npgsql;

namespace NetCoreWebGoat.Models
{
    public class PostModelList
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Photo { get; set; }

        public string UserPhoto { get; set; }

        public string UserName { get; set; }

        public string UserLastName { get; set; }

        public bool Owner { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string CreatedAtFormatted => CreatedAt.ToString("dd/MM/yy HH:mm:ss");

        public PostModelList() { }

        public PostModelList(NpgsqlDataReader dataReader) => FillFromDataReader(dataReader);

        public void FillFromDataReader(NpgsqlDataReader dataReader)
        {
            Id = DatabaseHelper.GetValueOrNull<int>(dataReader, "Id");
            Text = DatabaseHelper.GetValueOrNull<string>(dataReader, "Text");
            Photo = DatabaseHelper.GetValueOrNull<string>(dataReader, "Photo");
            UserPhoto = DatabaseHelper.GetValueOrNull<string>(dataReader, "UserPhoto");
            UserName = DatabaseHelper.GetValueOrNull<string>(dataReader, "Name");
            UserLastName = DatabaseHelper.GetValueOrNull<string>(dataReader, "LastName");
            UserId = DatabaseHelper.GetValueOrNull<int>(dataReader, "UserId");
            CreatedAt = DatabaseHelper.GetValueOrNull<DateTime>(dataReader, "CreatedAt");
            UpdatedAt = DatabaseHelper.GetValueOrNull<DateTime?>(dataReader, "UpdatedAt");
        }
    }
}