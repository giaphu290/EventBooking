using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Domain.Entities;
using EventBooking.Infrastructure.Persistences.DBContext;
using EventBooking.Infrastructure.Persistences.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Infrastructure.Persistences.Repositories
{
    public class EventTicketRepository : BaseRepository<EventTicket>, IEventTicketRepository
    {
        private readonly ApplicationDbContext _context;
        public EventTicketRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext; 
        }
    }
}
