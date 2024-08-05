using EventBooking.Application.Common.Constants;
using EventBooking.Application.Features.Auth.UserRoleManage.Queries;
using EventBooking.Domain.BaseException;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserRoleManage.Handlers
{
    public class GetUserRoleByUserIdHandler : IRequestHandler<GetUserRoleByUserIdQuery, List<string>>
    {
        private readonly UserManager<User> _userManager;

        public GetUserRoleByUserIdHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<string>> Handle(GetUserRoleByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy người dùng");
                }

                var roles = await _userManager.GetRolesAsync(user);
                return roles.ToList();
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
