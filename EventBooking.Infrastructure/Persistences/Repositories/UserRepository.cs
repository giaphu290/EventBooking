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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.ResetToken == refreshToken);
        }

        public async Task<IEnumerable<User>> GetUsersByHoVaTenAsync(string hoVaTen)
        {
            var searchTerm = hoVaTen.Trim().ToLower();
            var users = await _dbContext.Users
                                   .Where(u => u.Name.ToLower().Contains(searchTerm))
                                   .ToListAsync();
            return users;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async  Task UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
