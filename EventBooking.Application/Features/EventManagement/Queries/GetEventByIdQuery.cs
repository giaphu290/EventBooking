using EventBooking.Application.Features.EventManagement.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.EventManagement.Queries
{
    public class GetEventByIdValidator : AbstractValidator<GetEventByIdQuery>
    {
        public GetEventByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

        }
    }


    public class GetEventByIdQuery : IRequest<EventResponse>
    {
        public int Id { get; set; }
    }
}
