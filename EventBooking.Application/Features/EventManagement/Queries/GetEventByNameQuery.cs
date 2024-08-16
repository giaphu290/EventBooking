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
  

    public class GetEventByNameValidator : AbstractValidator<GetEventByNameQuery>
    {
        public GetEventByNameValidator()
        {
            RuleFor(m => m.Name).NotNull();
        }
    }


    public class GetEventByNameQuery : IRequest<IEnumerable<EventResponse>>
    {
        public string Name { get; set; }
    }
}
