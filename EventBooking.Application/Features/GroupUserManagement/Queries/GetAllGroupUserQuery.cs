using EventBooking.Application.Features.GroupUserManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.GroupUserManagement.Queries
{
    public class GetAllGroupUserQuery : IRequest<IEnumerable<GroupUserResponse>>
    {
    }
}
