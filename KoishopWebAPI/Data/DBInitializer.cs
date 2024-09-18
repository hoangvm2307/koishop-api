using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoishopBusinessObjects;
using KoishopRepositories;
using Microsoft.AspNetCore.Identity;

namespace KoishopWebAPI.Data
{
  public class DBInitializer
  {
    public static async Task Initialize(KoishopDBContext context, UserManager<User> userManager)
    {
      if (!userManager.Users.Any())
      {
        var user = new User
        {
          UserName = "bob",
          Email = "bobtest@gmail.com"
        };

        await userManager.CreateAsync(user, "Pa$$w0rd");
        await userManager.AddToRoleAsync(user, "Customer");
        var admin = new User
        {
          UserName = "admin",
          Email = "admin@gmail.com"
        };

        await userManager.CreateAsync(admin, "Pa$$w0rd");
        await userManager.AddToRolesAsync(admin, new[] { "Customer", "Admin" });
      }
      context.SaveChanges();
    }
  }
}