using EventBooking.Application.Features.Auth.LoginManage.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.LoginManage.Commands
{
   public class LoginGoogleValidator : AbstractValidator<LoginGoogleCommand>
    {
        public LoginGoogleValidator() {
            RuleFor(a => a.GoogleAccessToken).NotEmpty();   
        }
    }
    public class LoginGoogleCommand : IRequest<LoginResponse>
    {
        public string GoogleAccessToken { get; set; }

 
    }
}
