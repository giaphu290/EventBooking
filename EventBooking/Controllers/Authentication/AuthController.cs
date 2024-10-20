﻿using EventBooking.Application.Common.Bases;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.Auth.UserManage.Commands;
using EventBooking.Application.Features.Auth.UserManage.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EventBooking.Application.Features.Auth.LoginManage.Queries;
using EventBooking.Application.Features.Auth.LoginManage.Commands;
using EventBooking.Application.Features.Auth.LoginManage.Models;
using EventBooking.Application.Features.Auth.PasswordManage.Commands;

namespace EventBooking.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediatorService _mediatorService;
        public AuthController(IMediatorService mediatorService)
        {
            _mediatorService = mediatorService;
        }

        /// <summary>
        /// Lấy thông tin người dùng hiện tại.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-current-user")]
        [Authorize]
        public async Task<ActionResult<GetUserDetailResponse>> GetLoggedUser()
        {
            var response = await _mediatorService.Send(new GetCurrentUserQuery());
            return Ok(new BaseResponseModel(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }

        /// <summary>
        /// Đăng nhập.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            var response = await _mediatorService.Send(request);
            return Ok(new BaseResponseModel<LoginResponse>(
                StatusCodes.Status201Created,
                ResponseCodeConstants.SUCCESS,
                response));
        }

        /// <summary>
        /// Đăng ký
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] CreateUserCommand command)
        {
            var response = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel<CreateUserResponse>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }

        /// <summary>
        /// Cập nhật người dùng bởi quản trị viên.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("update-user-by-admin")]
        public async Task<ActionResult<CreateUserResponse>> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var response = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel<CreateUserResponse>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }

        /// <summary>
        /// Làm mới token. (Use for test)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand request)
        {
            var response = await _mediatorService.Send(request);
            return Ok(new BaseResponseModel<RefreshTokenResponse>(
                StatusCodes.Status201Created,
                ResponseCodeConstants.SUCCESS,
                response));
        }

        /// <summary>
        /// Quên mật khẩu.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand request)
        {
            var response = await _mediatorService.Send(request);
            return Ok(new BaseResponseModel<Unit>(
                StatusCodes.Status201Created,
                ResponseCodeConstants.SUCCESS,
                response));
        }

        /// <summary>
        /// Kiểm tra mã code hợp lệ để lấy lại mật khẩu.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("check-valid-code")]
        public async Task<IActionResult> CheckValidCode([FromBody] VerifyCodeCommand request)
        {
            var isValid = await _mediatorService.Send(request);
            return Ok(new BaseResponseModel<bool>(
                StatusCodes.Status201Created,
                ResponseCodeConstants.SUCCESS,
                isValid));
        }

        /// <summary>
        /// Đặt lại mật khẩu.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
        {
            var response = await _mediatorService.Send(request);
            return Ok(new BaseResponseModel<IdentityResult>(
                StatusCodes.Status201Created,
                ResponseCodeConstants.SUCCESS,
                response));
        }
    }
}

