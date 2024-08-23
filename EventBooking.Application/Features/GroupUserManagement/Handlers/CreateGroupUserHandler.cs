using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.GroupUserManagement.Commands;
using EventBooking.Application.Features.GroupUserManagement.Models;
using EventBooking.Application.Features.TicketManagement.Models;
using EventBooking.Domain.BaseException;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.GroupUserManagement.Handlers
{
    public class CreateGroupUserHandler : IRequestHandler<CreateGroupUserCommand, GroupUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;
        public CreateGroupUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;

            _timeService = timeService;
        }

        public async Task<GroupUserResponse> Handle(CreateGroupUserCommand request, CancellationToken cancellationToken)
        {


            try
            {
                var currentUserId = _userContextService.GetCurrentUserId();

                var groups = await _unitOfWork.GroupRepository.GetByIdAsync(request.GroupId)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy Group");
                if (groups.IsDelete || !groups.IsActive)
                {
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Group đã bị xoá hoặc không tìm thấy");
                }
                var users = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy User");
                if (users.IsDelete || !users.IsActive)
                {
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy User hoặc đã bị xoá");
                }
                var newGroupUser = _mapper.Map<GroupUser>(request);
                newGroupUser.CreatedBy = currentUserId;
                newGroupUser.LastUpdatedBy = currentUserId;
                newGroupUser.CreatedTime = _timeService.SystemTimeNow;
                newGroupUser.LastUpdatedTime = _timeService.SystemTimeNow;
                newGroupUser = await _unitOfWork.GroupUserRepository.AddAsync(newGroupUser);
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<GroupUserResponse>(newGroupUser);
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
