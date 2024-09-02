using EventBooking.Application.Common.Constants;
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
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly int _exAccessToken;
        private readonly int _exRefreshToken;
        private readonly ITimeService _timeService;

        public LoginHandler(IConfiguration configuration, UserManager<User> userManager, ITimeService timeService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _exAccessToken = int.Parse(_configuration["Jwt:ExpirationAccessToken"]!);
            _exRefreshToken = int.Parse(_configuration["Jwt:ExpirationRefreshToken"]!);
            _timeService = timeService;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.Username) ??
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Tên đăng nhập hoặc mật khẩu không đúng");

                var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!checkPassword)
                {
                    throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.UNAUTHENTICATED, "Tên đăng nhập hoặc mật khẩu không đúng");
                }

                var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
                         ?? throw new ErrorException(StatusCodes.Status403Forbidden, ResponseCodeConstants.UNAUTHORIZED, "Nguời dùng chưa được cấp quyền");

                // Generate access token
                var accessToken = GenerateToken(user.Id, userRole, false);
                // Generate refresh token
                var refreshToken = GenerateToken(user.Id, userRole, true);

                // Save Database
                user.VerificationToken = accessToken;
                user.ResetToken = refreshToken;
                user.VerificationTokenExpires = _timeService.SystemTimeNow.AddHours(_exRefreshToken);
                user.VerificationTokenExpires = _timeService.SystemTimeNow.AddHours(_exRefreshToken);
                user.ResetTokenExpires = _timeService.SystemTimeNow.AddMinutes(_exAccessToken);
                await _userManager.UpdateAsync(user);
                return new LoginResponse
                {
                    VerificationToken = accessToken,
                    ResetToken = refreshToken,
                    UserId = user.Id,
                    Name = user.UserName,
                    Email = user.Email,
                    Role = userRole
                };
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

        private string GenerateToken(string userId, string role, bool isRefreshToken)
        {
            try
            {
                var keyString = _configuration["Jwt:Key"]
                               ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "JWT key is not configured.");
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new("Id", userId),
                    new(ClaimTypes.NameIdentifier, userId),
                    new(ClaimTypes.Role, role),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                if (isRefreshToken)
                {
                    claims.Add(new Claim("isRefreshToken", "true"));
                }

                DateTime expiresDateTime;
                if (isRefreshToken)
                {
                    expiresDateTime = _timeService.SystemTimeNow.AddHours(_exRefreshToken).DateTime;
                }
                else
                {
                    expiresDateTime = _timeService.SystemTimeNow.AddMinutes(_exAccessToken).DateTime;
                }


                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: expiresDateTime,
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
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã xảy ra lỗi không mong muốn khi lưu.");
            }
        }
    }
}
