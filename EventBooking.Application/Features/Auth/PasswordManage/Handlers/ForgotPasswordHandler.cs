using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.Auth.PasswordManage.Commands;
using EventBooking.Domain.BaseException;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.PasswordManage.Handlers
{
    internal class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;

        public ForgotPasswordHandler(UserManager<User> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email) ??
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy người dùng");
                // Tạo mã xác thực
                var code = new Random().Next(1000, 9999).ToString();
                // Băm mã xác thực
                var codeHash = _userManager.PasswordHasher.HashPassword(user, code);

                user.EmailCode = codeHash;
                await _userManager.UpdateAsync(user);

                var selectedEmail = new List<string> { request.Email };
                await _emailService.SendEmailAsync(selectedEmail, "Password Reset Code", $"Your reset code is: {code}");
                return Unit.Value;
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã xảy ra lỗi không mong muốn khi lưu.");
            }
        }
    }
}
