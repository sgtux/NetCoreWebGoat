using System.ComponentModel.DataAnnotations;

namespace NetCoreWebGoat.Models
{
    public class UserRegisterModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(4)]
        public string Password { get; set; }

        [Required]
        public string Confirm { get; set; }

        public System.DateTime CreatedAt { get; set; }
    }
}