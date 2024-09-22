using System;
using System.Collections.Generic;
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
            // Check if any users exist in the database
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "BobtheBuilder",
                    Email = "BobtheBuilder@gmail.com",
                    UserName = "BobtheBuilder@gmail.com",
                    Address = new Address  
                    {
                        FirtName = "BobtheBuilder", 
                        LastName = "Bulby",
                        Street = "08 Lorenzo",
                        City = "BGC",
                        State = "PH",
                        ZipCode = "1920"
                    }
                };

                // Save the user to the database
                await userManager.CreateAsync(user, "Pa$$w0rd"); 
            }
        }
    }
}
