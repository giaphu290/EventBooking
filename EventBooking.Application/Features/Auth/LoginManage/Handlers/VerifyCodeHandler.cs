using EventBooking.Application.Common.Constants;
using EventBooking.Application.Features.Auth.LoginManage.Commands;
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

namespace EventBooking.Application.Features.Auth.LoginManage.Handlers
{
    public class VerifyCodeHandler : IRequestHandler<VerifyCodeCommand, bool>
    {
        private readonly UserManager<User> _userManager;

        public VerifyCodeHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(VerifyCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email) ??
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy người dùng");

                var verificationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.EmailCode, request.Code);
                return verificationResult != PasswordVerificationResult.Failed;
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
