﻿using EventBooking.Application.Common.Persistences.IRepositories.IBaseRepositories;
using EventBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Common.Persistences.IRepositories
{
    public interface IEventTicketRepository : IBaseRepository<EventTicket>
    {
        Task<IEnumerable<EventTicket>> GetEventsByEventIdAsync(int id);

    }
}
