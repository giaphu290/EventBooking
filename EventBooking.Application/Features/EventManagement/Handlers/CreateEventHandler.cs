using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.EventManagement.Commands;
using EventBooking.Application.Features.EventManagement.Models;
using EventBooking.Domain.BaseException;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.EventManagement.Handlers
{
    public class CreateEventHandler : IRequestHandler<CreateEventCommand, CreateEventResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly INormalizeVietnamese _normalizeVietnamese;
        private readonly ITimeService _timeService;
        public CreateEventHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, INormalizeVietnamese normalizeVietnamese, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
            _normalizeVietnamese = normalizeVietnamese;
            _timeService = timeService;
        }

        public async Task<CreateEventResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
           
            try
            {
                var currentUserId = _userContextService.GetCurrentUserId();
                var normalizedEventName = _normalizeVietnamese.NormalizeVietnamese(request.Name);
                var existingEvent = _unitOfWork.EventRepository.GetAllAsync().Result
                    .FirstOrDefault(e => _normalizeVietnamese.NormalizeVietnamese(e.Name)
                    .Equals(normalizedEventName, StringComparison.OrdinalIgnoreCase));
                if (existingEvent != null && !existingEvent.IsDelete) {

                    throw new ErrorException(StatusCodes.Status409Conflict, ResponseCodeConstants.DUPLICATE, "Sự kiện đã tồn tại");

                }
                if (existingEvent != null && existingEvent.IsDelete)
                {
                    existingEvent.IsActive = true;
                    existingEvent.IsDelete = false;
                    existingEvent.LastUpdatedBy = currentUserId;
                    existingEvent.LastUpdatedTime = _timeService.SystemTimeNow;
                    await _unitOfWork.SaveChangeAsync();
                    return _mapper.Map<CreateEventResponse>(request);

                }
                var newEvent = _mapper.Map<Event>(request);
                newEvent.HostId = currentUserId;
                newEvent.Status = EventStatus.Draft;
                newEvent.CreatedBy = currentUserId;
                newEvent.LastUpdatedBy = currentUserId;
                newEvent.CreatedTime = _timeService.SystemTimeNow;
                newEvent.LastUpdatedTime = _timeService.SystemTimeNow;
                newEvent = await _unitOfWork.EventRepository.AddAsync(newEvent);
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<CreateEventResponse>(newEvent);
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
