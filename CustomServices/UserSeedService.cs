using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Opera.Models;

namespace Opera.CustomServices
{
    public class UserSeedService
    {
        private UserManager<CustomUserFields> _userManager;

        public UserSeedService( UserManager<CustomUserFields> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedNewUser(string userName, string email, string password)
        {
            var _user = new CustomUserFields
            {
                UserName = userName,
                Email = email,
                FullName = "Brian Jerez",
                Description = "Ain't got no cash Ain't got no style. Ladies vomit when I smile",
                ImageUrl = "/img/static/avatar.png"
            };

            var result = await _userManager.CreateAsync(_user, password);
            
            if(result.Succeeded)
            {
                IdentityResult x = await _userManager.AddToRoleAsync(_user, "Administrador");
            }
        }
    }
}