using EventBooking.Application.Common.Persistences.IRepositories.IBaseRepositories;
using EventBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Common.Persistences.IRepositories
{
    public interface IEventInvitationRepository : IBaseRepository<EventInvitation>
    {
        Task<IEnumerable<EventInvitation>> GetEventInvitationsByEventIdAsync(int eventId);
        Task<IEnumerable<EventInvitation>> GetEventInvitationsByUserIdAsync(string userId);
    }
}
