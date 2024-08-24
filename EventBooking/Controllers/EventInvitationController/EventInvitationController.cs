using EventBooking.Application.Common.Bases;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.AllowedEventGroupManagement.Models;
using EventBooking.Application.Features.EventInvitationManagement.Commands;
using EventBooking.Application.Features.EventInvitationManagement.Models;
using EventBooking.Application.Features.EventInvitationManagement.Queries;
using EventBooking.Application.Features.EventManagement.Models;
using EventBooking.Application.Features.PostManagement.Commands;
using EventBooking.Application.Features.PostManagement.Models;
using EventBooking.Application.Features.PostManagement.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.API.Controllers.EventInvitationController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventInvitationController : ControllerBase
    {
        private readonly IMediatorService _mediatorService;
        public EventInvitationController(IMediatorService mediatorService)
        {
            _mediatorService = mediatorService;

        }
        /// <summary>
        /// Tạo mới lời mời người dùng về sự kiện.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateEventInvitation([FromBody] CreateEventInvitationCommand command)
        {
            var response = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel<EventInvitationResponse>
                (statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
        /// <summary>
        /// Cập nhật lời mời người dùng về sự kiện.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateEventInvitation([FromBody] UpdateEventInvitationCommand command)
        {
            var response = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel<EventInvitationResponse>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
        /// <summary>
        /// Xóa bài lời mời người dùng về sự kiện.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteEventInvitation([FromBody] DeleteEventInvitationCommand command)
        {
            bool response = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }

        /// <summary>
        /// Lấy danh sách tất cả lời mời người dùng về sự kiện
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        public async Task<ActionResult<EventResponse>> GetAllEventInvitation()
        {
            var query = new GetAllEventInvitationQuery();
            var response = await _mediatorService.Send(query);
            return Ok(new BaseResponseModel<IEnumerable<EventInvitationResponse>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }

        /// <summary>
        /// Lấy thông tin lời mời người dùng về sự kiện theo Id.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetEventInvitaionById([FromQuery] GetEventInvitationByIdQuery query)
        {
            var response = await _mediatorService.Send(query);
            return Ok(new BaseResponseModel<EventInvitationResponse>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
        /// <summary>
        /// Lấy thông tin  lời mời người dùng về sự kiện theo sự kiện Id.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("get-by-event-id")]
        public async Task<IActionResult> GetEventInvitationByEventId([FromQuery] GetEventInvitationByEventIdQuery query)
        {
            var response = await _mediatorService.Send(query);
            return Ok(new BaseResponseModel<IEnumerable<EventInvitationResponse>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
        /// <summary>
        /// Lấy thông tin  lời mời người dùng về sự kiện theo người dùng Id.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("get-by-user-id")]
        public async Task<IActionResult> GetEventInvitationByUserId([FromQuery] GetEventInvitationByUserIdQuery query)
        {
            var response = await _mediatorService.Send(query);
            return Ok(new BaseResponseModel<IEnumerable<EventInvitationResponse>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
    }
}
