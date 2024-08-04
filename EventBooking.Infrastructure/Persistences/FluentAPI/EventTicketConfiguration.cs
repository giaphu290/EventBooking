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
    public class EventTicketConfiguration : IEntityTypeConfiguration<EventTicket>
    {
        public void Configure(EntityTypeBuilder<EventTicket> builder)
        {
            builder.ToTable("EventTicket");
            builder.HasKey(t => t.Id);
            builder.Property(e => e.IsPaid).IsRequired();
            builder.Property(e => e.PurchaseDate).IsRequired();
            builder
           .HasOne(ut => ut.Event)
           .WithMany(u => u.EventTickets)
           .HasForeignKey(ut => ut.EventId)
           .OnDelete(DeleteBehavior.NoAction);
            builder
           .HasOne(ut => ut.User)
           .WithMany(u => u.EventTickets)
           .HasForeignKey(ut => ut.UserId)
           .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
