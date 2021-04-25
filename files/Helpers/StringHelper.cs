using System.Linq;

namespace NetCoreWebGoat.Helpers
{
    public class StringHelper
    {
        public static string GetProfilePhoto(System.Security.Claims.ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(p => p.Type == "Photo")?.Value ?? "profile-default.jpg";
        }
    }
}