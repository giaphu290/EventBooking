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
    public class CreateAllowedEventGroupHandler : IRequestHandler<CreateAllowedEventGroupCommand, AllowedEventGroupResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;

        public CreateAllowedEventGroupHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<AllowedEventGroupResponse> Handle(CreateAllowedEventGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currentUserId = _userContextService.GetCurrentUserId();
                var events = await _unitOfWork.EventRepository.GetByIdAsync(request.EventId) ??
                     throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy sự kiện");

                var groups = await _unitOfWork.GroupRepository.GetByIdAsync(request.GroupId) ?? 
                     throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Nhóm không tồn tại.");

                if (await _unitOfWork.AllowedEventGroupRepository.CheckInvitation(request.EventId,request.EventId))
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Lời mời đã được gửi.");
                }
                var newAllowedEventGroup = _mapper.Map<AllowedEventGroup>(request);
                newAllowedEventGroup.CreatedBy = currentUserId;
                newAllowedEventGroup.LastUpdatedBy = currentUserId;
                newAllowedEventGroup.CreatedTime = _timeService.SystemTimeNow;
                newAllowedEventGroup.LastUpdatedTime = _timeService.SystemTimeNow;
                newAllowedEventGroup = await _unitOfWork.AllowedEventGroupRepository.AddAsync(newAllowedEventGroup);

                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<AllowedEventGroupResponse>(newAllowedEventGroup);
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
