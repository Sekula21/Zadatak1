using Microsoft.AspNetCore.Identity;
using Zadatak1.Models;

namespace Zadatak1.Services
{
    public class MySeeder
    {
        public async Task SeedUser(UserManager<User> userManager)
        {
            var existingUser = await userManager.FindByNameAsync("admin");
            if (existingUser == null)
            {
                var user = new User
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    FirstName = "Admin",
                    LastName = "Account",
                    Gender = "Other",
                    BirthDate = new DateTime(1990, 1, 1),
                    UserRole = true,
                    LoginPermission = true
                };

                var result = await userManager.CreateAsync(user, "Admin123!");
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        Console.WriteLine(error.Description);
                }
            }
        }
    }
}
