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
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.EventManagement.Handlers
{
    public class UpdateEventHander : IRequestHandler<UpdateEventCommand, EventResponse>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;
        private readonly UserManager<User> _userManager;

        public UpdateEventHander(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, ITimeService timeService, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
            _userManager = userManager;
        }
        public async Task<EventResponse> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currentUserId = _userContextService.GetCurrentUserId();
                var existingEvent = await _unitOfWork.EventRepository.GetByIdAsync(request.Id)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Event not found.");

                // Check if event is active and not deleted
                if (existingEvent.IsDelete || !existingEvent.IsActive)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Event not found or inactive.");
                }

                // Validate the host role
                var existingUser = await _userManager.FindByIdAsync(request.HostId)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Host user not found.");

                var roles = await _userManager.GetRolesAsync(existingUser);
                if (!roles.Contains("host"))
                {
                    throw new ErrorException(StatusCodes.Status403Forbidden, ResponseCodeConstants.UNAUTHORIZED, "User does not have host privileges.");
                }

                // Validate dates
                //if (request.EndDate.HasValue && !request.StartDate.HasValue)
                //{
                //    if (request.EndDate <= existingEvent.StartDate)
                //    {
                //        throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "End date must be after the start date.");
                //    }
                //}

                //// Check StartDate if only StartDate is inputted
                //if (request.StartDate.HasValue && !request.EndDate.HasValue && existingEvent.EndDate.HasValue)
                //{
                //    if (request.StartDate >= existingEvent.EndDate.Value)
                //    {
                //        throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Start date must be before the end date.");
                //    }
                //}

                //// Check RegistrationEndDate if it's the only date inputted
                //if (request.RegistrationEndDate.HasValue && !request.StartDate.HasValue && !request.EndDate.HasValue)
                //{
                //    if (request.RegistrationEndDate >= existingEvent.StartDate)
                //    {
                //        throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Registration end date must be before or on the start date.");
                //    }
                //}

                // Map and update the event
                _mapper.Map(request, existingEvent);
                existingEvent.LastUpdatedBy = currentUserId;
                existingEvent.LastUpdatedTime = _timeService.SystemTimeNow;

                await _unitOfWork.SaveChangeAsync();

                return _mapper.Map<EventResponse>(existingEvent);
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
