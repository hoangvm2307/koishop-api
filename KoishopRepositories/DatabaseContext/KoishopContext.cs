using KoishopBusinessObjects;
using KoishopRepositories.DatabaseContext.Configurations;
using Microsoft.EntityFrameworkCore;

namespace KoishopRepositories.DatabaseContext;

public class KoishopContext : DbContext
{
  public KoishopContext(DbContextOptions<KoishopContext> options) : base(options)
  {

  }

  public DbSet<Breed> Breeds { get; set; }
  public DbSet<Consignment> Consignments { get; set; }
  public DbSet<ConsignmentItem> ConsignmentItems { get; set; }
  public DbSet<FishCare> FishCares { get; set; }
  public DbSet<KoiFish> KoiFishes { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<User> Users { get; set; }
  public DbSet<OrderItem> OrderItems { get; set; }
  public DbSet<Rating> Ratings { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<User>().ToTable("AspNetUsers", t=> t.ExcludeFromMigrations());
    // modelBuilder.ApplyConfiguration(new UserConfigurations());
    modelBuilder.ApplyConfiguration(new KoiFishConfigurations());
    modelBuilder.ApplyConfiguration(new OrderConfigurations());
    modelBuilder.ApplyConfiguration(new BreedConfigurations());
    ConfigureModel(modelBuilder);
  }

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
        .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified || q.State == EntityState.Deleted))
    {
      entry.Entity.DateModified = DateTime.UtcNow;
      //entry.Entity.ModifiedBy = _userService.UserId;
      if (entry.State == EntityState.Added)
      {
        entry.Entity.DateCreated = DateTime.UtcNow;
        //entry.Entity.CreatedBy = _userService.UserId;
      }
      else if (entry.State == EntityState.Deleted)
      {
        entry.Entity.isDeleted = true;
        entry.State = EntityState.Modified;
      }
    }
    return base.SaveChangesAsync(cancellationToken);
  }
  private void ConfigureModel(ModelBuilder modelBuilder)
  {


  }

}
