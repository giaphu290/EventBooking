using EventBooking.Application.Features.Auth.LoginManage.Models;
using FluentValidation;
using MediatR;


namespace EventBooking.Application.Features.Auth.LoginManage.Commands
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
    {
          public  RefreshTokenValidator() {
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("Yêu cầu refreshToken");
        
        
        }
    }
    public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
    {

        public string RefreshToken { get; set; }
    
    }
}
