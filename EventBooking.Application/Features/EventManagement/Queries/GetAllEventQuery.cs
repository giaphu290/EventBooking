using EventBooking.Application.Features.EventManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.EventManagement.Queries
{
    public class GetAllEventQuery : IRequest<IEnumerable<EventResponse>>
    {
    }
}
