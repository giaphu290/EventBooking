using EventBooking.Application.Common.Bases;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.Auth.UserManage.Commands;
using EventBooking.Application.Features.Auth.UserManage.Models;
using EventBooking.Application.Features.Auth.UserManage.Queries;
using EventBooking.Application.Features.Auth.UserRoleManage.Commands;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMediatorService _mediatorService;

        public UserController(IMediatorService mediatorService, IWebHostEnvironment webHostEnvironment)
        {
            _mediatorService = mediatorService;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Lấy người dùng theo họ và tên.
        /// </summary>
        /// <param name="hoVaTen"></param>
        /// <returns></returns>
        [HttpGet("get-by-name")]
        public async Task<IActionResult> GetUsersByHoVaTen(string hoVaTen)
        {
            var query = new GetUserByHoVaTenQuery(hoVaTen);
            var users = await _mediatorService.Send(query);
            if (users == null || !users.Any())
            {
                return NotFound();
            }
            return Ok(users);
        }

        /// <summary>
        /// Lấy tất cả người dùng.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-user")]
        public async Task<ActionResult<BaseResponseModel<IEnumerable<GetAllUserResponse>>>> GetAllUser()
        {
            var response = await _mediatorService.Send(new GetAllUserQuery());
            return Ok(new BaseResponseModel<IEnumerable<GetAllUserResponse>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }

        /// <summary>
        /// Xóa vai trò người dùng.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("delete-user-role")]
        public async Task<ActionResult> DeleteUserRole([FromBody] DeleteUserRoleCommand command)
        {
            var result = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel<bool>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
        /// <summary>
        /// Tải lên hình ảnh người dùng.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload/{id}")]
        public async Task<IActionResult> UploadImage(string id, IFormFile file)
        {
            var command = new UpdateUserImageCommand { UserId = id, File = file };
            var response = await _mediatorService.Send(command);

            if (!response.IsSuccess)
            {
                return NotFound("Không tìm thấy người dùng.");
            }

            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response.ImageUrl));
        }
        
    }
}