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
    public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
    {
        [Required(ErrorMessage = "RefreshToken không được để trống!")]
        public string RefreshToken { get; set; }

        public RefreshTokenCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
