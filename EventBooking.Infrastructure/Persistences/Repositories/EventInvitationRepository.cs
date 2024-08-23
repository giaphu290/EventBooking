using EventBooking.Application.Common.Persistences.IRepositories;
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

        public async Task<IEnumerable<EventInvitation>> GetEventInvitationsByEventIdAsync(int eventId)
        {
            return await _context.EventInvitations.Where(a => a.EventId == eventId && a.IsActive == true && a.IsDelete == false).ToListAsync();


        }

        public async Task<IEnumerable<EventInvitation>> GetEventInvitationsByUserIdAsync(string userId)
        {
            return await _context.EventInvitations
          .Where(t => t.UserId.Equals(userId) && t.IsActive == true && t.IsDelete == false)
          .ToListAsync();
        }
    }
}
