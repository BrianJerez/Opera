using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Opera.Models
{
    public class IdentityDataContext : IdentityDbContext<CustomUserFields>
    { 
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
        :base(options)
        {
            Database.EnsureCreated();
        }
    }
}