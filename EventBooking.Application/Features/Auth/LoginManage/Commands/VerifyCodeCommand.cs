using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace EventBooking.Application.Features.Auth.LoginManage.Commands
{
    public class VerifyCodeCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "Email không được để trống!")]
        [EmailAddress(ErrorMessage = "Sai định dạng Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Code không được để trống!")]
        public string Code { get; set; }

        public VerifyCodeCommand(string email, string code)
        {
            Email = email;
            Code = code;
        }
    }
}
