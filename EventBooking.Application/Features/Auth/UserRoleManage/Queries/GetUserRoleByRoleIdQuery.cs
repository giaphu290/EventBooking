using EventBooking.Application.Features.Auth.UserRoleManage.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserRoleManage.Queries
{
    public class GetUserRoleByRoleIdQuery : IRequest<List<string>>
    {
        public string RoleId { get; set; }
    }
}
