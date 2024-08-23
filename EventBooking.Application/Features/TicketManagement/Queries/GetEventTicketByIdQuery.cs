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
    public class GetEventTicketByIdValidator : AbstractValidator<GetEventTicketByIdQuery>
    {
        public GetEventTicketByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

        }
    }


    public class GetEventTicketByIdQuery : IRequest<EventTicketResponse>
    {
        public int Id { get; set; }
    }
}
