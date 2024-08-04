using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.Auth.RoleManage.Models;
using EventBooking.Application.Features.Auth.RoleManage.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        public async Task<ActionResult<RoleResponse>> GetAllRole()
        {
            var user = await _mediatorService.Send(new GetRoleQuery());
            return Ok(new BaseResponseModel<IEnumerable<GetRoleResponse>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: user));
        }
    }
}
