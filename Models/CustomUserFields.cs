using Microsoft.AspNetCore.Identity;

namespace Opera.Models
{
    public class CustomUserFields : IdentityUser
    {
        public string FullName { get; set; }
        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}