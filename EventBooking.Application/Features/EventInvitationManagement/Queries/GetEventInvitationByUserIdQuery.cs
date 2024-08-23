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
    internal class GetEventInvitationByUserIdValidator : AbstractValidator<GetEventInvitationByUserIdQuery>
    {
        public GetEventInvitationByUserIdValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Yêu cầu UserId");
        }
    }


    public class GetEventInvitationByUserIdQuery : IRequest<IEnumerable<EventInvitationResponse>>
    {
        public string UserId { get; set; }
    }
}
