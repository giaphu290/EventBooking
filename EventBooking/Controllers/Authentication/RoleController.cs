using EventBooking.Application.Common.Bases;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.Auth.RoleManage.Commands;
using EventBooking.Application.Features.Auth.RoleManage.Models;
using EventBooking.Application.Features.Auth.RoleManage.Queries;
using EventBooking.Application.Features.Auth.UserRoleManage.Model;
using EventBooking.Application.Features.Auth.UserRoleManage.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediatorService _mediatorService;

        public RoleController(IMediatorService mediatorService)
        {
            _mediatorService = mediatorService;
        }
        /// <summary>
        /// Lấy tất cả các vai trò (roles).
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        public async Task<ActionResult<RoleResponse>> GetAllRole()
        {
            var user = await _mediatorService.Send(new GetRoleQuery());
            return Ok(new BaseResponseModel<IEnumerable<RoleResponse>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: user));
        }
        /// <summary>
        /// Tạo mới một vai trò (role).
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AddRoleCommand command)
        {
            var result = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel<bool>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
        /// <summary>
        /// Cập nhật thông tin vai trò (role).
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateRoleCommand command)
        {
            var result = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel<bool>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
        /// <summary>
        /// Xóa vai trò (role).
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteRoleCommand command)
        {
            var result = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel<bool>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
        /// <summary>
        /// Lấy tất cả các cặp UserId và RoleId của người dùng và vai trò.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-user-role")]
        public async Task<ActionResult<IEnumerable<(string UserId, string RoleId)>>> GetAllUserRole()
        {
            var query = new GetAllUserRoleQuery();
            var result = await _mediatorService.Send(query);
            return Ok(new BaseResponseModel<IEnumerable<UserRoleResponse>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
        /// <summary>
        /// Lấy danh sách vai trò của người dùng theo UserId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("get-user-role-by-userId/{userId}")]
        public async Task<ActionResult<IEnumerable<IdentityUserRole<string>>>> GetByUserId(string userId)
        {
            var query = new GetUserRoleByUserIdQuery { UserId = userId };
            var result = await _mediatorService.Send(query);
            return Ok(new BaseResponseModel<IEnumerable<string>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        /// <summary>
        /// Lấy danh sách người dùng thuộc vai trò theo RoleId.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("get-user-role-by-roleId/{roleId}")]
        public async Task<ActionResult<IEnumerable<IdentityUserRole<string>>>> GetByRoleId(string roleId)
        {
            var query = new GetUserRoleByRoleIdQuery { RoleId = roleId };
            var result = await _mediatorService.Send(query);
            return Ok(new BaseResponseModel<IEnumerable<string>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
    }
}
