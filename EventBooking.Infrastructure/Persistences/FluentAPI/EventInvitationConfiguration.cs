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
    public class EventInvitationConfiguration : IEntityTypeConfiguration<EventInvitation>
    {
        public void Configure(EntityTypeBuilder<EventInvitation> builder)
        {
            builder.ToTable("EventInvitation");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.SentDate).IsRequired();
            builder
           .HasOne(ut => ut.User)
           .WithMany(u => u.EventInvitations)
           .HasForeignKey(ut => ut.UserId)
           .OnDelete(DeleteBehavior.NoAction);
            builder
           .HasOne(ut => ut.Event)
           .WithMany(u => u.EventInvitations)
           .HasForeignKey(ut => ut.EventId)
           .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
