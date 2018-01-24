using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Opera.Models;
using Opera.CustomServices;

namespace Opera
{
    public class Startup
    {
        private string _OperaString = @"Data Source=DESKTOP-2U6NEI8\MSSQLSERVER01;Initial Catalog=Opera;Integrated Security=True;Pooling=False";
        private string _identityString = @"Data Source=DESKTOP-2U6NEI8\MSSQLSERVER01;Initial Catalog=Opera_Identity;Integrated Security=True;Pooling=False";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OperaDataContext>(options => {
                options.UseSqlServer(_OperaString);
            });

            services.AddDbContext<IdentityDataContext>(options => {
                options.UseSqlServer(_identityString);
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>{
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<IdentityDataContext>();

            services.ConfigureApplicationCookie(options =>{
                options.LoginPath = "/";
            });

            services.AddSingleton<RoleSeedService>();
            services.AddSingleton<UserSeedService>();
            services.AddTransient<MarkDownService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            var seedRoles = services.GetService<RoleSeedService>();
            var seedUsers = services.GetService<UserSeedService>();

            seedRoles.SeedRoles("Administrador").Wait();
            seedRoles.SeedRoles("Moderador").Wait();
            seedRoles.SeedRoles("Usuario").Wait();

            app.UseMvc(routes => {
                routes.MapRoute("default", "{controller=LandingPages}/{action=Index}/{id?}");
            });

            app.UseStaticFiles();

            await seedUsers.SeedNewUser("Admin", "Admin@gmail.com", "password1");
        }
    }
}
