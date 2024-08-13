using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.PasswordManage.Commands
{
    public class ForgotPasswordCommand : IRequest<Unit>
    {
        [Required(ErrorMessage = "Email không được để trống!")]
        [EmailAddress(ErrorMessage = "Sai định dạng Email.")]
        public string Email { get; set; }

        public ForgotPasswordCommand(string email)
        {
            Email = email;
        }
    }
}
