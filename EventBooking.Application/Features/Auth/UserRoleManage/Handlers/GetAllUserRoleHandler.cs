using EventBooking.Application.Common.Constants;
using EventBooking.Application.Features.Auth.UserRoleManage.Model;
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
    public class GetAllUserRoleHandler : IRequestHandler<GetAllUserRoleQuery, IEnumerable<UserRoleResponse>>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public GetAllUserRoleHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<UserRoleResponse>> Handle(GetAllUserRoleQuery request, CancellationToken cancellationToken)
        {
            var allUserRoles = new List<UserRoleResponse>();
            try
            {

                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        var roleEntity = await _roleManager.FindByNameAsync(role);
                        if (roleEntity != null)
                        {
                            allUserRoles.Add(new UserRoleResponse { UserId = user.Id, RoleId = roleEntity.Id });
                        }
                    }
                }
                return allUserRoles;
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã có lỗi xảy ra");
            }

        }
    }
}
