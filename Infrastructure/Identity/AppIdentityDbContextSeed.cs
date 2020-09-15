using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser {
                    DisplayName = "Mueen",
                    Email = "moenali@test.com",
                    UserName = "moenali@test.com",
                    Address = new Address {
                        FirstName = "Mueen",
                        LastName = "Ali",
                        Street = "20 Bukit Jalil",
                        City = "Kuala Lumpur",
                        State = "Selangor",
                        ZipCode = "57000"
                    }
                };

                await userManager.CreateAsync(user, "Mueen@0");
            }
        }
    }
}