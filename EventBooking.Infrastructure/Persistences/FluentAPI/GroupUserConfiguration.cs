using EventBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Infrastructure.Persistences.FluentAPI
{
    public class GroupUserConfiguration : IEntityTypeConfiguration<GroupUser>
    {
        public void Configure(EntityTypeBuilder<GroupUser> builder)
        {
            builder.ToTable("GroupUser");
            builder.HasKey(t => t.Id);
            builder.Property(a => a.UserId).IsRequired();
            builder.Property(a => a.GroupId).IsRequired();
            builder
           .HasOne(ut => ut.Group)
           .WithMany(u => u.GroupUsers)
           .HasForeignKey(ut => ut.GroupId)
           .OnDelete(DeleteBehavior.NoAction);
            builder
           .HasOne(ut => ut.User)
           .WithMany(u => u.GroupUsers)
           .HasForeignKey(ut => ut.UserId)
           .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
