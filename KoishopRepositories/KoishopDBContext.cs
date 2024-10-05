using KoishopBusinessObjects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KoishopRepositories
{
  public class KoishopDBContext : IdentityDbContext<User, Role, int>
  {
    public KoishopDBContext(DbContextOptions<KoishopDBContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      builder.Entity<Role>()
            .HasData(
                new Role { Id = 1, Name = "Guest", NormalizedName = "GUEST" },
                new Role { Id = 2, Name = "Customer", NormalizedName = "CUSTOMER" },
                new Role { Id = 3, Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Id = 4, Name = "Staff", NormalizedName = "STAFF" }
            );
      builder.Entity<User>()
              .Property(u => u.Id)
              .ValueGeneratedOnAdd();
    }
  }
}