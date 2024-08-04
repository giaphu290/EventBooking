using MediatR;
using System.ComponentModel.DataAnnotations;


namespace EventBooking.Application.Features.Auth.RoleManage.Commands
{
    public class AddRoleCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "Role name is required")]
        [MinLength(1, ErrorMessage = "Role name must be at least 1 character long")]
        public string? Name { get; set; }
    }
}
