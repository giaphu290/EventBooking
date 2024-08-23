using EventBooking.Application.Features.EventInvitationManagement.Models;
using EventBooking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.EventInvitationManagement.Queries
{
    public class GetAllEventInvitationQuery : IRequest<IEnumerable<EventInvitationResponse>>
    {
    }
}
