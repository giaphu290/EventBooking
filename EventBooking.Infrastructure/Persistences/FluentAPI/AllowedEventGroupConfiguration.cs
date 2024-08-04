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
    public class AllowedEventGroupConfiguration : IEntityTypeConfiguration<AllowedEventGroup>
    {
        public void Configure(EntityTypeBuilder<AllowedEventGroup> builder)
        {
            builder.ToTable("AllowedEventGroup");
            builder.HasKey(e => e.Id);
            builder
           .HasOne(ut => ut.Event)
           .WithMany(u => u.AllowedEventGroups)
           .HasForeignKey(ut => ut.EventId)
           .OnDelete(DeleteBehavior.NoAction);
            builder
           .HasOne(ut => ut.Group)
           .WithMany(u => u.AllowedEventGroups)
           .HasForeignKey(ut => ut.GroupId)
           .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
