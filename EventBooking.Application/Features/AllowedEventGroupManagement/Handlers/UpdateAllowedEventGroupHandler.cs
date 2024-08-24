using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.AllowedEventGroupManagement.Commands;
using EventBooking.Application.Features.AllowedEventGroupManagement.Models;
using EventBooking.Domain.BaseException;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.AllowedEventGroupManagement.Handlers
{
    public class UpdateAllowedEventGroupHandler : IRequestHandler<UpdateAllowedEventGroupCommand, AllowedEventGroupResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;
        public UpdateAllowedEventGroupHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<AllowedEventGroupResponse> Handle(UpdateAllowedEventGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var groupInvitations = await _unitOfWork.AllowedEventGroupRepository.GetByIdAsync(request.Id);
                if (groupInvitations == null || groupInvitations.IsDelete || !groupInvitations.IsActive)
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy lời mời nhóm hoặc đang inactive");

                var events = await _unitOfWork.EventRepository.GetByIdAsync(request.EventId);
                if (events == null || events.IsDelete || !events.IsActive)
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy sự kiện");
                var groups = await _unitOfWork.GroupRepository.GetByIdAsync(request.GroupId);
                if (groups == null || groups.IsDelete || !groups.IsActive)
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy nhóm");
                _mapper.Map(request, groupInvitations);
                groupInvitations.LastUpdatedBy = _userContextService.GetCurrentUserId();
                groupInvitations.LastUpdatedTime = _timeService.SystemTimeNow;
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<AllowedEventGroupResponse>(groupInvitations);
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
