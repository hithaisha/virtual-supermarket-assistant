using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MORR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MORR.Infrastructure.Data.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");

            builder.HasKey(p => new { p.UserId, p.RoleId });

            builder.HasOne<User>(f => f.User)
                   .WithMany(f => f.UserRoles)
                   .HasForeignKey(f => f.UserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            builder.HasOne<Role>(f => f.Role)
                   .WithMany(f => f.UserRoles)
                   .HasForeignKey(f => f.RoleId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);
        }
    }
}
