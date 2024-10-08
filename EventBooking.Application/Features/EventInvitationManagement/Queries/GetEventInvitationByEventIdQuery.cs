﻿using EventBooking.Application.Features.EventInvitationManagement.Models;
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
    public class GetEventInvitationByEventIdValidator : AbstractValidator<GetEventInvitationByEventIdQuery>
    {
        public GetEventInvitationByEventIdValidator()
        {
            RuleFor(x => x.EventId).GreaterThan(0);

        }
    }


    public class GetEventInvitationByEventIdQuery : IRequest<IEnumerable<EventInvitationResponse>>
    {
        public int EventId { get; set; }
    }
}
