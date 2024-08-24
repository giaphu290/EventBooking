using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.Auth.LoginManage.Commands;
using EventBooking.Application.Features.Auth.LoginManage.Models;
using EventBooking.Domain.BaseException;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventBooking.Application.Features.Auth.LoginManage.Handlers
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly ITimeService _timeService;

        public RefreshTokenHandler(IConfiguration configuration, IUnitOfWork unitOfWork, UserManager<User> userManager, ITimeService timeService)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _timeService = timeService;
        }
        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetUserByRefreshTokenAsync(request.RefreshToken);
                if (user == null || user.ResetTokenExpires <= _timeService.SystemTimeNow)
                {
                    throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.UNAUTHORIZED, "Invalid or expired refresh token");
                }
                var roles = await _userManager.GetRolesAsync(user);
                var userRole = roles.FirstOrDefault();
                if (userRole == null)
                {
                    throw new SecurityTokenException("User dont have permission");
                }
                var newAccessToken = GenerateAccessToken(user.Id, userRole);
                var _exAccessToken = int.Parse(_configuration["Jwt:ExpirationAccessToken"]!);

                // save database
                user.VerificationToken = newAccessToken;
                user.VerificationTokenExpires = _timeService.SystemTimeNow.AddMinutes(_exAccessToken);
                await _unitOfWork.UserRepository.UpdateUserAsync(user);
                //
                return new RefreshTokenResponse
                {
                    VerificationToken = newAccessToken,
                    RefreshToken = user.ResetToken,
                };
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã xảy ra lỗi");
            }
        }
        private string GenerateAccessToken(string userId, string role)
        {
            try
            {
                var _exAccessToken = int.Parse(_configuration["Jwt:ExpirationAccessToken"]!);

                var keyString = _configuration["Jwt:Key"]
                    ?? throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.UNAUTHORIZED, "JWT key is not configured.");
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new("Id", userId),
                    new(ClaimTypes.Role, role),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new("isRefreshToken", "false")
                };
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: _timeService.SystemTimeNow.AddMinutes(_exAccessToken).DateTime,
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã xảy ra lỗi");
            }
        }
    }
}
