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
    public class BreedConfigurations : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.BreedName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.ScreeningRatio)
                .HasMaxLength(50);

            builder.Property(b => b.Personality)
                .HasMaxLength(200);

            builder.HasMany(b => b.KoiFish)
                .WithOne(k => k.Breed)
                .HasForeignKey(k => k.BreedId);
        }
    }
}
