using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Features.Auth.UserManage.Commands;
using EventBooking.Application.Features.Auth.UserManage.Models;
using EventBooking.Domain.BaseException;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserManage.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public CreateUserHandler(IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _userManager.Users.AnyAsync(x => x.UserName == request.Username))
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.DUPLICATE, "User đã tồn tại.");

                var roleName = request.RoleName.Trim();
                IdentityRole? role = await _roleManager.FindByNameAsync(roleName);
                if (role == null)
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Vai trò không tồn tại.");

                User newUser = _mapper.Map<User>(request);

                IdentityResult resultCreateUser = await _userManager.CreateAsync(newUser, request.Password);
                if (!resultCreateUser.Succeeded)
                    throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã có lỗi xảy ra");

                IdentityResult resultAddRole = await _userManager.AddToRoleAsync(newUser, role.Name!);
                if (!resultAddRole.Succeeded)
                    throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã có lỗi xảy ra");

                return _mapper.Map<CreateUserResponse>(newUser);
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
