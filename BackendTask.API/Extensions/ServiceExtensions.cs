using BackendTask.Data.DbContexts;
using BackendTask.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BackendTask.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<SchoolContext>()
            .AddDefaultTokenProviders();
        }
    }
}
