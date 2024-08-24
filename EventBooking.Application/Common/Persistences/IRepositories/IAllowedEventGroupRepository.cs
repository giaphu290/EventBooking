using EventBooking.Application.Common.Persistences.IRepositories.IBaseRepositories;
using EventBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Common.Persistences.IRepositories
{
    public interface IAllowedEventGroupRepository : IBaseRepository<AllowedEventGroup>
    {

        Task<bool> CheckInvitation(int eventId, int groupId);
        Task<IEnumerable<AllowedEventGroup>> GetAllowedEventGroupsByEventIdAsync(int id);
        Task<IEnumerable<AllowedEventGroup>> GetAllowedEventGroupsByGroupIdAsync(int id);

    }
}
