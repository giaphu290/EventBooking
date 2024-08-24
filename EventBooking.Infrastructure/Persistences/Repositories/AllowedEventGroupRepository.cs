
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Domain.Entities;
using EventBooking.Infrastructure.Persistences.DBContext;
using EventBooking.Infrastructure.Persistences.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Infrastructure.Persistences.Repositories
{
    public class AllowedEventGroupRepository : BaseRepository<AllowedEventGroup>, IAllowedEventGroupRepository
    {
        private readonly ApplicationDbContext _context;
        public AllowedEventGroupRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }


        public async Task<bool> CheckInvitation(int eventId, int groupId)
        {
            return await _context.AllowedEvents.AnyAsync(a => a.EventId == eventId && a.GroupId == groupId);
        }

        public async Task<IEnumerable<AllowedEventGroup>> GetAllowedEventGroupsByEventIdAsync(int id)
        {
            return await _context.AllowedEvents.Where(a => a.EventId == id && a.IsDelete == false && a.IsActive == true).ToListAsync();

        }

        public async Task<IEnumerable<AllowedEventGroup>> GetAllowedEventGroupsByGroupIdAsync(int id)
        {
            return await _context.AllowedEvents.Where(a => a.GroupId == id && a.IsDelete == false && a.IsActive == true).ToListAsync();
        }
    }
}
