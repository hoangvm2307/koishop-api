using KoishopBusinessObjects;
using KoishopRepositories;
using KoishopRepositories.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KoishopWebAPI.Data
{
  public class DBInitializer
  {
    public static async Task Initialize(KoishopDBContext identityContext, KoishopContext persistenceContext, UserManager<User> userManager)
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

      // Thêm dữ liệu mẫu cho Breed nếu chưa có
      if (!persistenceContext.Breeds.Any())
      {
        var breeds = new List<Breed>
        {
          new Breed { BreedName = "Kohaku", ScreeningRatio = "1:4", Personality = "Active" },
          new Breed { BreedName = "Sanke", ScreeningRatio = "1:5", Personality = "Calm" },
          new Breed { BreedName = "Showa", ScreeningRatio = "1:6", Personality = "Strong" },
          new Breed { BreedName = "Asagi", ScreeningRatio = "1:3", Personality = "Shy" },
          new Breed { BreedName = "Shusui", ScreeningRatio = "1:4", Personality = "Curious" },
          new Breed { BreedName = "Tancho", ScreeningRatio = "1:5", Personality = "Friendly" },
          new Breed { BreedName = "Bekko", ScreeningRatio = "1:3", Personality = "Lively" },
          new Breed { BreedName = "Utsuri", ScreeningRatio = "1:4", Personality = "Peaceful" },
          new Breed { BreedName = "Goshiki", ScreeningRatio = "1:5", Personality = "Energetic" },
          new Breed { BreedName = "Kujaku", ScreeningRatio = "1:4", Personality = "Approachable" }
        };

        persistenceContext.Breeds.AddRange(breeds);
        await persistenceContext.SaveChangesAsync();
      }

      // Thêm dữ liệu mẫu cho KoiFish nếu chưa có
      if (!persistenceContext.KoiFishes.Any())
      {
        var breeds = await persistenceContext.Breeds.ToListAsync();

        var koiFishes = new List<KoiFish>
        {
          new KoiFish { Name = "Sakura", Origin = "Japan", Gender = "Female", Age = 2, Size = 25.5m, Personality = "Friendly", DailyFoodAmount = 0.5m, Type = "Ornamental", Price = 1000m, ListPrice = 1200m, ImageUrl = "https://example.com/sakura.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Kohaku").Id },
          new KoiFish { Name = "Haru", Origin = "Japan", Gender = "Male", Age = 3, Size = 30.0m, Personality = "Active", DailyFoodAmount = 0.6m, Type = "Show", Price = 1500m, ListPrice = 1800m, ImageUrl = "https://example.com/haru.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Sanke").Id },
          new KoiFish { Name = "Yuki", Origin = "China", Gender = "Female", Age = 1, Size = 20.0m, Personality = "Shy", DailyFoodAmount = 0.4m, Type = "Pond", Price = 800m, ListPrice = 950m, ImageUrl = "https://example.com/yuki.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Showa").Id },
          new KoiFish { Name = "Kenta", Origin = "Japan", Gender = "Male", Age = 4, Size = 35.5m, Personality = "Strong", DailyFoodAmount = 0.7m, Type = "Butterfly", Price = 2000m, ListPrice = 2300m, ImageUrl = "https://example.com/kenta.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Asagi").Id },
          new KoiFish { Name = "Midori", Origin = "Indonesia", Gender = "Female", Age = 2, Size = 28.0m, Personality = "Curious", DailyFoodAmount = 0.5m, Type = "Longfin", Price = 1200m, ListPrice = 1400m, ImageUrl = "https://example.com/midori.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Shusui").Id },
          new KoiFish { Name = "Taro", Origin = "Japan", Gender = "Male", Age = 3, Size = 32.0m, Personality = "Calm", DailyFoodAmount = 0.6m, Type = "Jumbo", Price = 1700m, ListPrice = 2000m, ImageUrl = "https://example.com/taro.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Tancho").Id },
          new KoiFish { Name = "Hana", Origin = "Thailand", Gender = "Female", Age = 1, Size = 22.5m, Personality = "Lively", DailyFoodAmount = 0.4m, Type = "Premium", Price = 900m, ListPrice = 1050m, ImageUrl = "https://example.com/hana.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Bekko").Id },
          new KoiFish { Name = "Kenji", Origin = "Japan", Gender = "Male", Age = 5, Size = 40.0m, Personality = "Peaceful", DailyFoodAmount = 0.8m, Type = "Ornamental", Price = 2500m, ListPrice = 2800m, ImageUrl = "https://example.com/kenji.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Utsuri").Id },
          new KoiFish { Name = "Akira", Origin = "Vietnam", Gender = "Female", Age = 2, Size = 26.5m, Personality = "Energetic", DailyFoodAmount = 0.5m, Type = "Show", Price = 1100m, ListPrice = 1300m, ImageUrl = "https://example.com/akira.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Goshiki").Id },
          new KoiFish { Name = "Ryu", Origin = "Japan", Gender = "Male", Age = 3, Size = 33.5m, Personality = "Approachable", DailyFoodAmount = 0.7m, Type = "Pond", Price = 1800m, ListPrice = 2100m, ImageUrl = "https://example.com/ryu.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Kujaku").Id },
          new KoiFish { Name = "Sora", Origin = "China", Gender = "Male", Age = 2, Size = 27.5m, Personality = "Playful", DailyFoodAmount = 0.5m, Type = "Butterfly", Price = 1300m, ListPrice = 1500m, ImageUrl = "https://example.com/sora.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Kohaku").Id },
          new KoiFish { Name = "Emi", Origin = "Japan", Gender = "Female", Age = 3, Size = 31.0m, Personality = "Gentle", DailyFoodAmount = 0.6m, Type = "Longfin", Price = 1600m, ListPrice = 1900m, ImageUrl = "https://example.com/emi.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Sanke").Id },
          new KoiFish { Name = "Kai", Origin = "Israel", Gender = "Male", Age = 4, Size = 36.5m, Personality = "Adventurous", DailyFoodAmount = 0.7m, Type = "Jumbo", Price = 2100m, ListPrice = 2400m, ImageUrl = "https://example.com/kai.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Showa").Id },
          new KoiFish { Name = "Nami", Origin = "Japan", Gender = "Female", Age = 1, Size = 21.0m, Personality = "Timid", DailyFoodAmount = 0.4m, Type = "Premium", Price = 850m, ListPrice = 1000m, ImageUrl = "https://example.com/nami.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Asagi").Id },
          new KoiFish { Name = "Hiroshi", Origin = "USA", Gender = "Male", Age = 5, Size = 39.0m, Personality = "Majestic", DailyFoodAmount = 0.8m, Type = "Ornamental", Price = 2400m, ListPrice = 2700m, ImageUrl = "https://example.com/hiroshi.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Shusui").Id },
          new KoiFish { Name = "Yumi", Origin = "Japan", Gender = "Female", Age = 2, Size = 26.0m, Personality = "Graceful", DailyFoodAmount = 0.5m, Type = "Show", Price = 1150m, ListPrice = 1350m, ImageUrl = "https://example.com/yumi.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Tancho").Id },
          new KoiFish { Name = "Takeshi", Origin = "Thailand", Gender = "Male", Age = 3, Size = 33.0m, Personality = "Bold", DailyFoodAmount = 0.6m, Type = "Pond", Price = 1750m, ListPrice = 2050m, ImageUrl = "https://example.com/takeshi.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Bekko").Id },
          new KoiFish { Name = "Aiko", Origin = "Japan", Gender = "Female", Age = 1, Size = 23.5m, Personality = "Curious", DailyFoodAmount = 0.4m, Type = "Butterfly", Price = 950m, ListPrice = 1100m, ImageUrl = "https://example.com/aiko.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Utsuri").Id },
          new KoiFish { Name = "Daichi", Origin = "Vietnam", Gender = "Male", Age = 4, Size = 37.5m, Personality = "Dominant", DailyFoodAmount = 0.7m, Type = "Longfin", Price = 2200m, ListPrice = 2500m, ImageUrl = "https://example.com/daichi.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Goshiki").Id },
          new KoiFish { Name = "Mei", Origin = "Japan", Gender = "Female", Age = 2, Size = 29.0m, Personality = "Elegant", DailyFoodAmount = 0.5m, Type = "Jumbo", Price = 1400m, ListPrice = 1600m, ImageUrl = "https://example.com/mei.jpg", Status = "Available", BreedId = breeds.First(b => b.BreedName == "Kujaku").Id }
        };

        persistenceContext.KoiFishes.AddRange(koiFishes);
        await persistenceContext.SaveChangesAsync();
      }

      identityContext.SaveChanges();
    }
  }
}