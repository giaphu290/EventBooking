using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.EventInvitationManagement.Commands;
using EventBooking.Application.Features.EventInvitationManagement.Models;
using EventBooking.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.EventInvitationManagement.Handlers
{
    public class UpdateEventInvitationHandler : IRequestHandler<UpdateEventInvitationCommand, EventInvitationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;
        public UpdateEventInvitationHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<EventInvitationResponse> Handle(UpdateEventInvitationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var eventInvitations = await _unitOfWork.EventInvitationRepository.GetByIdAsync(request.Id);
                if (eventInvitations == null || eventInvitations.IsDelete || !eventInvitations.IsActive)
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy lời mời sự kiện hoặc đang inactive");
                var currentUserId = _userContextService.GetCurrentUserId();

                var events = await _unitOfWork.EventRepository.GetByIdAsync(request.EventId)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy sự kiện");
                if (events.IsDelete || !events.IsActive)
                {
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Sự kiện đã bị xoá hoặc không tìm thấy");
                }

                var users = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy người dùng");
                if (users.IsDelete || !users.IsActive)
                {
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Người dùng đã bị xoá hoặc không tìm thấy");
                }
                if(request.ResponseType != eventInvitations.ResponseType)
                {
                    request.ResponseDate = _timeService.SystemTimeNow.DateTime;
                }    
                _mapper.Map(request, eventInvitations);
                eventInvitations.LastUpdatedBy = currentUserId;
                eventInvitations.LastUpdatedTime = _timeService.SystemTimeNow;
                await _unitOfWork.SaveChangeAsync();

                return _mapper.Map<EventInvitationResponse>(eventInvitations);
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
