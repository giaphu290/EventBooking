using EventBooking.Application.Features.EventManagement.Models;
using EventBooking.Application.Features.PostManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.PostManagement.Queries
{
    public class GetAllEventPostQuery : IRequest<IEnumerable<EventPostResponse>>
    {
    }
}
