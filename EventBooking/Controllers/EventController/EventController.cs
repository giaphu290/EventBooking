using EventBooking.Application.Common.Bases;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.EventManagement.Commands;
using EventBooking.Application.Features.EventManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.API.Controllers.EventController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediatorService _mediatorService;
        public EventController(IMediatorService mediatorService)
        {
            _mediatorService = mediatorService;
          
        }
        /// <summary>
        /// Tạo mới sự kiện.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command)
        {
            var response = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel<EventResponse>
                (statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
        /// <summary>
        /// Cập nhật thông tin sự kiện.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateDuAn([FromBody] UpdateEventCommand command)
        {
            var response = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel<EventResponse>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
        /// <summary>
        /// Xóa dự án.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteDuAn([FromBody] DeleteEventCommand command)
        {
            bool response = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
    }
}
