using EventBooking.Application.Common.Persistences.IRepositories.IBaseRepositories;
using EventBooking.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Common.Persistences.IRepositories
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IAllowedEventGroupRepository AllowedEventGroupRepository { get; }
        IEventInvitationRepository EventInvitationRepository { get; }
        IEventPostRepository EventPostRepository { get; }
        IEventRepository EventRepository { get; }
        IEventTicketRepository EventTicketRepository { get; }   
        IGroupRepository GroupRepository { get; }
        IGroupUserRepository GroupUserRepository { get; }
        IUserRepository UserRepository { get; }
        IBaseRepository<T> GetRepository<T>() where T : class, IBaseEntity;
        Task CommitAsync();
    }
}
