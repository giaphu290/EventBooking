using EventBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Infrastructure.Persistences.FluentAPI
{
    public class EventPostConfiguration : IEntityTypeConfiguration<EventPost>
    {
        public void Configure(EntityTypeBuilder<EventPost> builder)
        {
            builder.ToTable("EventPost");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.EventId).IsRequired();
            builder.Property(e => e.UserId).IsRequired();   
            builder.Property(e => e.Content).HasMaxLength(400);
            builder.Property(e => e.CreatedAt).IsRequired();
            builder
            .HasOne(ut => ut.User)
            .WithMany(u => u.EventPosts)
            .HasForeignKey(ut => ut.UserId)
            .OnDelete(DeleteBehavior.NoAction);
            builder
            .HasOne(ut => ut.Event)
            .WithMany(u => u.EventPosts)
            .HasForeignKey(ut => ut.EventId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
