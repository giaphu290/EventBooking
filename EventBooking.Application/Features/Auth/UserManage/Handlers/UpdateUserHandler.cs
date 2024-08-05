using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.Auth.UserManage.Commands;
using EventBooking.Application.Features.Auth.UserManage.Models;
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

namespace EventBooking.Application.Features.Auth.UserManage.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, CreateUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITimeService _timeService;
        public UpdateUserHandler( IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ITimeService timeService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _timeService = timeService;
        }
        public async Task<CreateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newUser = await _userManager.FindByIdAsync(request.Id);

                if (newUser == null)
                    throw new ArgumentNullException(nameof(request.Id), "Không tìm thấy người dùng");

                if (!string.IsNullOrEmpty(request.HoVaTen))
                    newUser.Name = request.HoVaTen;
                if (!string.IsNullOrEmpty(request.Bio) && request.Bio != newUser.Bio)
                {
                    newUser.Bio = request.Bio;
                }
                if (!string.IsNullOrEmpty(request.Email) && request.Email != newUser.Email)
                {
                    newUser.Email = request.Email;
                }

                if (!string.IsNullOrEmpty(request.PhoneNumber) && request.PhoneNumber != newUser.PhoneNumber)
                {
                    newUser.PhoneNumber = request.PhoneNumber;
                }

                User userUpdate = _mapper.Map<User>(newUser);
                userUpdate.LastUpdatedTime = _timeService.SystemTimeNow;
                var resultUpdate = await _userManager.UpdateAsync(userUpdate);

                if (!resultUpdate.Succeeded)
                    throw new InvalidOperationException(resultUpdate.Errors.FirstOrDefault()?.Description);
                //=====================================================================================================
                // Get current roles
                var currentRoles = await _userManager.GetRolesAsync(newUser);

                // Find new role
                if (!string.IsNullOrEmpty(request.RoleName))
                {
                    var roleName = request.RoleName.Trim();
                    var newRole = await _roleManager.FindByNameAsync(roleName);
                    if (newRole == null)
                    {
                        throw new ArgumentException("Không tìm thấy vai trò");
                    }

                    // Remove current roles
                    var removeResult = await _userManager.RemoveFromRolesAsync(newUser, currentRoles);
                    if (!removeResult.Succeeded)
                    {
                        throw new InvalidOperationException($"Lỗi xóa vai trò người dùng: {removeResult.Errors.FirstOrDefault()?.Description}");
                    }

                    // Add new role
                    var addResult = await _userManager.AddToRoleAsync(newUser, newRole.Name);
                    if (!addResult.Succeeded)
                    {
                        throw new InvalidOperationException($"Lỗi khi thêm người dùng vào vai trò: {addResult.Errors.FirstOrDefault()?.Description}");
                    }
                }
                //=====================================================================================================
                return _mapper.Map<CreateUserResponse>(userUpdate);
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
