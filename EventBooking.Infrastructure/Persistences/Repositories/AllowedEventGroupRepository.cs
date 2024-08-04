
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
    }
}
