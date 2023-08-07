using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MORR.Domain.Entities;

namespace MORR.Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.ItemCode)
                   .IsUnique(true);

            builder.Property(p => p.ItemName)
                   .IsRequired(false);

            builder.Property(p => p.IsDeleted)
                   .HasDefaultValue(false);

            builder.Property(p => p.DisplayInFrontPage)
                   .HasDefaultValue(false);

            builder.Property(p => p.IsShowWeb)
                   .HasDefaultValue(false);

            builder.Property(p => p.ShortDescription)
                   .IsRequired(false);

            builder.Property(p => p.LongDescription)
                   .IsRequired(false);

            builder.Property(p => p.ItemImageUrl)
                   .IsRequired(false);

            builder.Property(p=>p.Price)
                   .HasPrecision(6, 2);

            builder.Property(p => p.LoyalityPoints)
                   .HasPrecision(18, 2);

            builder.Property(p => p.Longitude)
                   .HasPrecision(18, 2);

            builder.Property(p => p.Latitude)
                   .HasPrecision(18, 2);

            builder.HasOne<User>(f => f.CreatedByUser)
               .WithMany(f => f.CreatedProduct)
               .HasForeignKey(f => f.CreatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne<Category>(f => f.Category)
                .WithMany(f => f.Products)
                .HasForeignKey(f => f.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>(f => f.UpdatedByUser)
                .WithMany(f => f.UpdatedProduct)
                .HasForeignKey(f => f.UpdatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
