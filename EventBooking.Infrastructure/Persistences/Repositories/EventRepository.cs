using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Persistences.IRepositories.IBaseRepositories;
using EventBooking.Domain.Entities;
using EventBooking.Infrastructure.Persistences.DBContext;
using EventBooking.Infrastructure.Persistences.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Infrastructure.Persistences.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Event>> GetEventsByNameAsync(string name)
        {
            return await _context.Events
              .Where(t => t.Name.ToLower().Trim().Contains(name.Trim().ToLower()) && t.IsActive == true && t.IsDelete == false)
              .ToListAsync();
        }
    }
}
