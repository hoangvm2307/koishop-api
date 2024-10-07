using KoishopBusinessObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopRepositories.DatabaseContext.Configurations
{
    public class KoiFishConfigurations : IEntityTypeConfiguration<KoiFish>
    {
        public void Configure(EntityTypeBuilder<KoiFish> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(k => k.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(k => k.Origin)
                .HasMaxLength(100);

            builder.Property(k => k.Gender)
                .HasMaxLength(10);

            builder.Property(k => k.Age)
                .IsRequired();

            builder.Property(k => k.Size)
                .IsRequired();

            builder.Property(k => k.Personality)
                .HasMaxLength(200);

            builder.Property(k => k.DailyFoodAmount)
                .IsRequired();

            builder.Property(k => k.Type)
                .HasMaxLength(50);

            builder.Property(k => k.Price)
                .IsRequired();

            builder.Property(k => k.ListPrice)
                .IsRequired();

            builder.Property(k => k.ImageUrl)
                .HasMaxLength(200);

            builder.Property(k => k.Status)
                .HasMaxLength(50);

            builder.HasMany(k => k.OrderItems)
                .WithOne(oi => oi.KoiFish)
                .HasForeignKey(oi => oi.KoiFishId);

            builder.HasMany(k => k.ConsignmentItems)
                .WithOne(ci => ci.KoiFish)
                .HasForeignKey(ci => ci.KoiFishId);

            builder.HasMany(k => k.Ratings)
                .WithOne(r => r.KoiFish)
                .HasForeignKey(r => r.KoiFishId);

            builder.HasMany(k => k.FishCare)
                .WithOne(fc => fc.KoiFish)
                .HasForeignKey(fc => fc.KoiFishId);
        }
    }
}
