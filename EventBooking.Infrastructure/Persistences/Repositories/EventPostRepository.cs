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
    public class EventPostRepository : BaseRepository<EventPost>, IEventPostRepository
    {
        private readonly ApplicationDbContext _context;
        public EventPostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<EventPost>> GetEventPostsByEventIdAsync(int id)
        {
            return await _context.EventPosts.Where(a => a.EventId == id && a.IsDelete == false  && a.IsActive == true).ToListAsync();
        }
    }
}
