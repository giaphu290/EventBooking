using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserRoleManage.Commands
{
    public class DeleteUserRoleCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public DeleteUserRoleCommand(string userId, string roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
