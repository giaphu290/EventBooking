using EventBooking.Application.Features.Auth.RoleManage.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.RoleManage.Queries
{
    public class GetRoleQuery : IRequest<IEnumerable<RoleResponse>>
    {
    }
}
