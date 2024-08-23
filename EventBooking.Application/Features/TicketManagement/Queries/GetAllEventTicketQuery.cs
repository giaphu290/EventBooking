using EventBooking.Application.Features.TicketManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.TicketManagement.Queries
{
    public class GetAllEventTicketQuery : IRequest<IEnumerable<EventTicketResponse>>
    {
    }
}
