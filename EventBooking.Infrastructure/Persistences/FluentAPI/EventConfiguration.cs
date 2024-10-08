﻿using EventBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Infrastructure.Persistences.FluentAPI
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(100);
            builder.Property(e => e.Price)
              .HasColumnType("decimal(18, 2)");
            builder
           .HasOne(ut => ut.User)
           .WithMany(u => u.Events)
           .HasForeignKey(ut => ut.HostId)
           .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
