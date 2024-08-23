using EventBooking.Application.Features.PostManagement.Models;
using EventBooking.Application.Features.TicketManagement.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.TicketManagement.Queries
{
    public class GetEventTicketByEventIdValidator : AbstractValidator<GetEventTicketByEventIdQuery>
    {
        public GetEventTicketByEventIdValidator()
        {
            RuleFor(x => x.EventId).GreaterThan(0);

        }
    }


    public class GetEventTicketByEventIdQuery : IRequest<IEnumerable<EventTicketResponse>>
    {
        public int EventId { get; set; }
    }
}
