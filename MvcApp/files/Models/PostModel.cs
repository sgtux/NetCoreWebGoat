using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using NetCoreWebGoat.Helpers;
using Npgsql;

namespace NetCoreWebGoat.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Text { get; set; }

        public string Photo { get; set; }

        public IFormFile File { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public PostModel() { }

        public PostModel(NpgsqlDataReader dataReader) => FillFromDataReader(dataReader);

        public void FillFromDataReader(NpgsqlDataReader dataReader)
        {
            Id = DatabaseHelper.GetValueOrNull<int>(dataReader, "Id");
            Text = DatabaseHelper.GetValueOrNull<string>(dataReader, "Text");
            Photo = DatabaseHelper.GetValueOrNull<string>(dataReader, "Photo");
            UserId = DatabaseHelper.GetValueOrNull<int>(dataReader, "UserId");
            CreatedAt = DatabaseHelper.GetValueOrNull<DateTime>(dataReader, "CreatedAt");
            UpdatedAt = DatabaseHelper.GetValueOrNull<DateTime?>(dataReader, "UpdatedAt");
        }
    }
}