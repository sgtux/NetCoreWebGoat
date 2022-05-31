using System;

namespace NetCoreWebGoat.Models
{
    public class UserModelResult
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Photo { get; set; }

        public int Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public UserModelResult() { }

        public UserModelResult(UserModel userModel)
        {
            Id = userModel.Id;
            Name = userModel.Name;
            LastName = userModel.LastName;
            Email = userModel.Email;
            Photo = userModel.Photo;
            Role = userModel.Role;
            CreatedAt = userModel.CreatedAt;
            UpdatedAt = userModel.UpdatedAt;
        }
    }
}