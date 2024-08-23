using EventBooking.Application.Common.Persistences.IRepositories.IBaseRepositories;
using EventBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Common.Persistences.IRepositories
{
    public interface IGroupUserRepository : IBaseRepository<GroupUser>
    {
        Task<IEnumerable<GroupUser>> GetGroupUserByGroupIdAsync(int groupId);
        Task<IEnumerable<GroupUser>> GetGroupUserByUserIdAsync(string userId);
    }
}
