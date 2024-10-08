﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserRoleManage.Commands
{
    public class AddUserToRoleCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }

        public AddUserToRoleCommand(string userId, string roleName)
        {
            UserId = userId;
            RoleName = roleName;
        }
    }
}
