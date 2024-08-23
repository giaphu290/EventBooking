using EventBooking.Application.Common.Bases;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.EventManagement.Commands;
using EventBooking.Application.Features.EventManagement.Models;
using EventBooking.Application.Features.EventManagement.Queries;
using EventBooking.Application.Features.TicketManagement.Commands;
using EventBooking.Application.Features.TicketManagement.Models;
using EventBooking.Application.Features.TicketManagement.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.API.Controllers.TicketController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    { 
        private readonly IMediatorService _mediatorService;
        public TicketController(IMediatorService mediatorService)
        {
            _mediatorService = mediatorService;

        }
        /// <summary>
        /// Tạo mới vé .
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateTicket([FromBody] CreateEventTicketCommand command)
        {
            var response = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel<EventTicketResponse>
                (statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
        /// <summary>
        /// Cập nhật thông tin vé.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateTicket([FromBody] UpdateEventTicketCommand command)
        {
            var response = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel<EventTicketResponse>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
        /// <summary>
        /// Xóa vé .
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTicket([FromBody] DeleteEventTicketCommand command)
        {
            bool response = await _mediatorService.Send(command);
            return Ok(new BaseResponseModel(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }

        /// <summary>
        /// Lấy danh sách tất cả vé
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        public async Task<ActionResult<EventTicketResponse>> GetAllTicket()
        {
            var query = new GetAllEventTicketQuery();
            var response = await _mediatorService.Send(query);
            return Ok(new BaseResponseModel<IEnumerable<EventTicketResponse>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }

        /// <summary>
        /// Lấy thông tin vé theo sự kiện.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("get-by-event-id")]
        public async Task<IActionResult> GetTicketByEventId([FromQuery] GetEventTicketByEventIdQuery query)
        {
            var response = await _mediatorService.Send(query);
            return Ok(new BaseResponseModel<IEnumerable<EventTicketResponse>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
        /// <summary>
        /// Lấy thông tin vé theo Id.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetTicketById([FromQuery] GetEventTicketByIdQuery query)
        {
            var response = await _mediatorService.Send(query);
            return Ok(new BaseResponseModel<EventTicketResponse>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: response));
        }
    }
}
