using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Opera.CustomServices
{
    public class RoleSeedService
    {
        private RoleManager<IdentityRole> _roleManager;

        public RoleSeedService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task SeedRoles(string RoleName)
        {
            if(!_roleManager.RoleExistsAsync(RoleName).Result)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = RoleName, NormalizedName = RoleName.ToString()});
            }
        }
    }
}