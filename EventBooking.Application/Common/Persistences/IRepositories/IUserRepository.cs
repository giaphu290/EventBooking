using EventBooking.Application.Common.Persistences.IRepositories.IBaseRepositories;
using EventBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Common.Persistences.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetUsersByHoVaTenAsync(string hoVaTen);
        Task<User> GetByIdAsync(string id);
        Task UpdateAsync(User user);
        Task<User> GetUserByRefreshTokenAsync(string refreshToken);
        Task UpdateUserAsync(User user);
    }
}
