﻿using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Domain.Entities;
using EventBooking.Infrastructure.Persistences.DBContext;
using EventBooking.Infrastructure.Persistences.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Infrastructure.Persistences.Repositories
{
    public class EventInvitationRepository : BaseRepository<EventInvitation>, IEventInvitationRepository
    {
        private readonly ApplicationDbContext _context;
        public EventInvitationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
