using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.Auth.LoginManage.Commands;
using EventBooking.Application.Features.Auth.LoginManage.Models;
using EventBooking.Domain.BaseException;
using EventBooking.Domain.Entities;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.LoginManage.Handlers
{
    public class LoginGoogleHandler : IRequestHandler<LoginGoogleCommand, LoginResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly ITimeService _timeService;
        private readonly int _exAccessToken;
        private readonly int _exRefreshToken;

        public LoginGoogleHandler(IConfiguration configuration, UserManager<User> userManager, ITimeService timeService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _timeService = timeService;
            _exAccessToken = int.Parse(_configuration["Jwt:ExpirationAccessToken"]!);
            _exRefreshToken = int.Parse(_configuration["Jwt:ExpirationRefreshToken"]!);
       
        }

        public async Task<LoginResponse> Handle(LoginGoogleCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var payload = await VerifyGoogleAccessTokenAsync(request.GoogleAccessToken);
                if (payload == null)
                {
                    throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.UNAUTHORIZED, "Invalid Google token");
                }

                var user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new User
                    {
                        UserName = payload.Email,
                        Name = payload.Name,
                        Email = payload.Email,
                        EmailConfirmed = true,

                    };
                    var result = await _userManager.CreateAsync(user);
                    if (!result.Succeeded)
                    {
                        throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Failed to create user.");
                    }
                }
                
                var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                if (userRole == null)
                {
                    userRole = "guest";
                    IdentityResult resultAddRole = await _userManager.AddToRoleAsync(user, userRole);
                    if (!resultAddRole.Succeeded)
                        throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã có lỗi xảy ra");
                   
                }
                var accessToken = GenerateToken(user.Id, userRole, false);
                var refreshToken = GenerateToken(user.Id, userRole, true);

                user.VerificationToken = accessToken;
                user.ResetToken = refreshToken;
                user.VerificationTokenExpires = _timeService.SystemTimeNow.AddMinutes(_exAccessToken);
                user.ResetTokenExpires = _timeService.SystemTimeNow.AddHours(_exRefreshToken);
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
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "An unexpected error occurred during Google login.");

            }
        }

        private async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleAccessTokenAsync(string accessToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new List<string> { _configuration["Authentication:Google:ClientId"] }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(accessToken, settings);
                return payload;
            }
            catch
            {
                return null;
            }
        }

        private string GenerateToken(string userId, string role, bool isRefreshToken)
        {
            var keyString = _configuration["Jwt:Key"]
                ?? throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.UNAUTHORIZED, "JWT key is not configured.");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new("Id", userId),
                new(ClaimTypes.Role, role),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (isRefreshToken)
            {
                claims.Add(new Claim("isRefreshToken", "true"));
            }

            var expires = isRefreshToken ? _timeService.SystemTimeNow.AddHours(_exRefreshToken) : _timeService.SystemTimeNow.AddMinutes(_exAccessToken);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires.DateTime,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
