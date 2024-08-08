using EventBooking.Application.Features.Auth.LoginManage.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.LoginManage.Commands
{
   public class LoginCommand : IRequest<LoginResponse>
    {
        [Required(ErrorMessage = "Username không được để trống!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        public string Password { get; set; }

        public LoginCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
