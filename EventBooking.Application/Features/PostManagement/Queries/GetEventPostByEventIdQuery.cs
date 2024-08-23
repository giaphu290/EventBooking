using EventBooking.Application.Features.PostManagement.Models;
using EventBooking.Application.Features.TicketManagement.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.PostManagement.Queries
{
    public class GetEventPostByEventIdValidator : AbstractValidator<GetEventPostByEventIdQuery>
    {
        public GetEventPostByEventIdValidator()
        {
            RuleFor(x => x.EventId).GreaterThan(0);

        }
    }


    public class GetEventPostByEventIdQuery : IRequest<IEnumerable<EventPostResponse>>
    {
        public int EventId { get; set; }
    }
}
