using GrandeTravel.Models;
using Microsoft.AspNetCore.Identity;
using System.Configuration.Provider;

namespace GrandeTravel.Data
{
    public static class GTDbSeed
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            UserManager<MyUser> userManager = serviceProvider.GetRequiredService<UserManager<MyUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
           
            if (await roleManager.FindByNameAsync("Customer") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            //Create Admin Role
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            //Create Travel Provider Role
            if (await roleManager.FindByNameAsync("TravelProvider") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("TravelProvider"));
            }
           /* await SeedTravelPackage(_TravelPackageRepo);*/
        }
    }
}
