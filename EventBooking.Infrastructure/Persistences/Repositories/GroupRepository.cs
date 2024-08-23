using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Domain.Entities;
using EventBooking.Infrastructure.Persistences.DBContext;
using EventBooking.Infrastructure.Persistences.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Infrastructure.Persistences.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        private readonly ApplicationDbContext _context;
        public GroupRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Group>> GetGroupsByNameAsync(string name)
        {
            return await _context.Groups
           .Where(t => t.Name.ToLower().Trim().Contains(name.Trim().ToLower()) && t.IsActive == true && t.IsDelete == false)
           .ToListAsync();
        }
    }
}
