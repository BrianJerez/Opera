using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Opera.CustomServices
{
    public class UserSeedService
    {
        private UserManager<IdentityUser> _userManager;

        public UserSeedService( UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedNewUser(string userName, string email, string password)
        {
            var _user = new IdentityUser
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