using EventBooking.Application.Features.EventInvitationManagement.Models;
using EventBooking.Application.Features.PostManagement.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.EventInvitationManagement.Queries
{
    public class GetEventInvitationByIdValidator : AbstractValidator<GetEventInvitationByIdQuery>
    {
        public GetEventInvitationByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

        }
    }


    public class GetEventInvitationByIdQuery : IRequest<EventInvitationResponse>
    {
        public int Id { get; set; }
    }
}
