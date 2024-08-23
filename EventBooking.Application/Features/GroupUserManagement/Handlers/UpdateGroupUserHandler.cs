using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.GroupManagement.Commands;
using EventBooking.Application.Features.GroupManagement.Models;
using EventBooking.Application.Features.GroupUserManagement.Commands;
using EventBooking.Application.Features.GroupUserManagement.Models;
using EventBooking.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.GroupUserManagement.Handlers
{
    public class UpdateGroupUserHandler : IRequestHandler<UpdateGroupUserCommand, GroupUserResponse>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;
        public UpdateGroupUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
        }
        public async Task<GroupUserResponse> Handle(UpdateGroupUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currentUserId = _userContextService.GetCurrentUserId();
                var existingGroupUser = await _unitOfWork.GroupRepository.GetByIdAsync(request.Id)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy Group User");

                // Check if event is active and not deleted
                if (existingGroupUser.IsDelete || !existingGroupUser.IsActive)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Group User không tồn tại");
                }
                if (request.GroupId.HasValue)
                {
                    var groups = await _unitOfWork.GroupRepository.GetByIdAsync(request.GroupId.Value)
                     ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy Group User");
                    if (groups.IsDelete || !groups.IsActive)
                    {
                        throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Group User không tồn tại");
                    }
                }
                if (!request.UserId.IsNullOrEmpty())
                {
                    var users = await _unitOfWork.GroupRepository.GetByIdAsync(request.UserId)
                     ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy Group User");
                    if (users.IsDelete || !users.IsActive)
                    {
                        throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Group User không tồn tại");
                    }
                }


                // Validate the host role
                _mapper.Map(request, existingGroupUser);
                existingGroupUser.LastUpdatedBy = currentUserId;
                existingGroupUser.LastUpdatedTime = _timeService.SystemTimeNow;

                await _unitOfWork.SaveChangeAsync();

                return _mapper.Map<GroupUserResponse>(existingGroupUser);
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "An unexpected error occurred.");
            }
        }
    }
}
