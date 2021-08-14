using Microsoft.AspNetCore.Http;

namespace NetCoreWebGoat.Models
{
    public class UserChangeModel
    {
        public int Id { get; set; }

        public string Photo { get; set; }

        public IFormFile File { get; set; }

        public int UserId { get; set; }
    }
}