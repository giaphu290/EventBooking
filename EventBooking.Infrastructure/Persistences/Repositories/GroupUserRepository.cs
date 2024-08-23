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
    public class GroupUserRepository : BaseRepository<GroupUser>, IGroupUserRepository
    {
        private readonly ApplicationDbContext _context;
        public GroupUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<GroupUser>> GetGroupUserByGroupIdAsync(int groupId)
        {
            return await _context.GroupUsers.Where(a => a.GroupId == groupId && a.IsDelete == false && a.IsActive == true).ToListAsync();
        }

        public async Task<IEnumerable<GroupUser>> GetGroupUserByUserIdAsync(string userId)
        {
            return await _context.GroupUsers
            .Where(t => t.UserId.Equals(userId) && t.IsActive == true && t.IsDelete == false)
            .ToListAsync();
        }
    }
}
