﻿using EventBooking.Application.Features.Auth.RoleManage.Commands;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using EventBooking.Domain.BaseException;
using Azure;
using EventBooking.Application.Common.Constants;

namespace EventBooking.Application.Features.Auth.RoleManage.Handlers
{
    public class AddRoleHandler : IRequestHandler<AddRoleCommand, bool>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public AddRoleHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<bool> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(request.Name.ToLower()));
                return result.Succeeded;
            }catch (ErrorException ex)
            {
                throw;
            }
            catch(Exception ex){
                throw new ErrorException(StatusCodes.Status500InternalServerError,ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã xảy ra lỗi không mong muốn khi lưu.");
            }
        }
    }
}
