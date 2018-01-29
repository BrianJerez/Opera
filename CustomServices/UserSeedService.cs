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
                Email = email
            };

            var result = await _userManager.CreateAsync(_user, password);
            
            if(result.Succeeded)
            {
                IdentityResult x = await _userManager.AddToRoleAsync(_user, "Administrador");
            }
        }
    }
}