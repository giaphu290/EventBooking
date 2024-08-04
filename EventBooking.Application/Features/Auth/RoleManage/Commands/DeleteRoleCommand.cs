using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.RoleManage.Commands
{
    public class DeleteRoleCommand : IRequest<bool> 
    {
        [Required(ErrorMessage = "Role name is required")]
        [MinLength(1, ErrorMessage = "Role name must be at least 1 character long")]
        public string? Name { get; set; }
    }
}
