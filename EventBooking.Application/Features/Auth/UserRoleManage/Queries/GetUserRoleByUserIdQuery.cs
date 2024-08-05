using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserRoleManage.Queries
{
    public class GetUserRoleByUserIdQuery : IRequest<List<string>>
    {
        public string UserId { get; set; }
    }
}
