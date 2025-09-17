using Microsoft.AspNetCore.Identity;
using RefugeUA.DatabaseAccess.Identity;

namespace RefugeUA.WebApp.Server.Data
{
    public static class SeedRoles
    {
        public static async Task SeedRolesAsync(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
            var roles = new List<string>
            {
                "MilitaryOrFamily",
                "LocalCitizen",
                "Volunteer",
                "CommunityAdmin",
                "Admin"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new AppRole(role));
                }
            }
        }
    }
}
