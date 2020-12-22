using C64.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Seeder.Pages
{
    public partial class Seed
    {
        private async Task AddRoles()
        {
            var adminRole = new IdentityRole("Admin");
            var moderatorRole = new IdentityRole("Moderator");
            await roleManager.CreateAsync(adminRole);
            await roleManager.CreateAsync(moderatorRole);
        }

        private async Task AddAdminUser()
        {
            var user = new User
            {
                UserName = model.AdminHandle,
                Email = model.AdminEmail,
            };

            var result = await userManager.CreateAsync(user, model.AdminPassword);
            await userManager.AddToRolesAsync(user, new string[] { "Admin", "Moderator" });
        }

        private async Task AddUsers()
        {
            for (var i = 1; i < 10; i++)
            {
                var guid = $"{i}{i}{i}{i}{i}{i}{i}{i}-{i}{i}{i}{i}-{i}{i}{i}{i}-{i}{i}{i}{i}-{i}{i}{i}{i}{i}{i}{i}{i}{i}{i}{i}{i}";
                userIds.Add(guid);
                var user = new User { Id = guid, UserName = $"TestUser{i}", Email = $"TestUser{i}@sampledb.com" };
                var result = await userManager.CreateAsync(user, "123456");
            }
        }
    }
}