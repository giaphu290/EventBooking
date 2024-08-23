using EventBooking.Application.Common.Bases;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.EventManagement.Commands;
using EventBooking.Application.Features.EventManagement.Models;
using EventBooking.Application.Features.EventManagement.Queries;
using EventBooking.Application.Features.GroupUserManagement.Commands;
using EventBooking.Application.Features.GroupUserManagement.Models;
using EventBooking.Application.Features.GroupUserManagement.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.API.Controllers.GroupController
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupUserController : ControllerBase
    {
        private readonly IMediatorService _mediatorService;
        public GroupUserController(IMediatorService mediatorService)
        {
            _mediatorService = mediatorService;

        }
        /// <summary>
        /// Tạo mới thành viên nhóm.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateGroupUser([FromBody] CreateGroupUserCommand command)
        {
            var response = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel<GroupUserResponse>
                (statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
        /// <summary>
        /// Cập nhật người dùng nhóm.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateGroupUserCommand command)
        {
            var response = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel<GroupUserResponse>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
        /// <summary>
        /// Xóa người dùng nhóm.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteGroupUser([FromBody] DeleteGroupUserCommand command)
        {
            bool response = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }

        /// <summary>
        /// Lấy danh sách người dùng nhóm
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        public async Task<ActionResult<GroupUserResponse>> GetAllGroupUser()
        {
            var query = new GetAllGroupUserQuery();
            var response = await _mediatorService.Send(query);
            return Ok(new BaseResponseModel<IEnumerable<GroupUserResponse>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }

        /// <summary>
        /// Lấy thông tin thành viên nhóm theo Id.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetGroupUserById([FromQuery] GetGroupUserByIdQuery query)
        {
            var response = await _mediatorService.Send(query);
            return Ok(new BaseResponseModel<GroupUserResponse>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
        /// <summary>
        /// Lấy thông tin người dùng nhóm theo người dùng id.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("get-by-user-id")]
        public async Task<IActionResult> GetGroupUserByUserId([FromQuery] GetGroupUserByUserIdQuery query)
        {
            var response = await _mediatorService.Send(query);
            return Ok(new BaseResponseModel<GroupUserResponse>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
        /// <summary>
        /// Lấy thông tin người dùng nhóm theo nhóm id.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("get-by-group-id")]
        public async Task<IActionResult> GetGroupUserByGroupId([FromQuery] GetGroupUserByGroupIdQuery query)
        {
            var response = await _mediatorService.Send(query);
            return Ok(new BaseResponseModel<IEnumerable<GroupUserResponse>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
    }
}
