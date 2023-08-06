using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MORR.Domain.Entities;

namespace MORR.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                   .IsRequired(false);

            builder.HasIndex(x => x.UserName)
                   .IsUnique(true);

            builder.HasOne<User>(f => f.CreatedByUser)
                   .WithMany(f => f.CreatedUsers)
                   .HasForeignKey(f => f.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(false);

            builder.HasOne<User>(f => f.UpdatedByUser)
                   .WithMany(f => f.UpdatedUsers)
                   .HasForeignKey(f => f.UpdatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(false);
        }
    }
}
