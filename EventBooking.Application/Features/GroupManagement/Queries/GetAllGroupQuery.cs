using EventBooking.Application.Features.EventManagement.Models;
using EventBooking.Application.Features.GroupManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.GroupManagement.Queries
{
    public class GetAllGroupQuery : IRequest<IEnumerable<GroupResponse>>
    {
    }
}
