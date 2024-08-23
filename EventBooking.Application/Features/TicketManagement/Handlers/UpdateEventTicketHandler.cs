using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.PostManagement.Commands;
using EventBooking.Application.Features.PostManagement.Models;
using EventBooking.Application.Features.TicketManagement.Models;
using EventBooking.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.TicketManagement.Handlers
{
    public class UpdateEventTicketHandler : IRequestHandler<UpdateEventTicketCommand, EventTicketResponse>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;
        public UpdateEventTicketHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
        }
        public async Task<EventTicketResponse> Handle(UpdateEventTicketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currentUserId = _userContextService.GetCurrentUserId();
                var existingTicket = await _unitOfWork.EventTicketRepository.GetByIdAsync(request.Id)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy vé");

                if (existingTicket.IsDelete || !existingTicket.IsActive)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Vé không tồn tại");
                }
                if (request.EventId.HasValue)
                {
                    var existingEvent = await _unitOfWork.EventRepository.GetByIdAsync(request.EventId)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy sự kiện");

                    if (existingEvent.IsDelete || !existingEvent.IsActive)
                    {
                        throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Sự kiện không tồn tại");
                    }
                }
                 if (!request.UserId.IsNullOrEmpty())
                    {
                        var existingUser = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId)
                        ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy người dùng");

                        if (existingUser.IsDelete || !existingUser.IsActive)
                        {
                            throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Người dùng không tồn tại");
                        }
                    }
                

                // Validate the host role
                _mapper.Map(request, existingTicket);
                if(existingTicket.IsPaid == true)
                {
                    existingTicket.PurchaseDate = _timeService.SystemTimeNow.DateTime;
                }    
                existingTicket.LastUpdatedBy = currentUserId;
                existingTicket.LastUpdatedTime = _timeService.SystemTimeNow;

                await _unitOfWork.SaveChangeAsync();

                return _mapper.Map<EventTicketResponse>(existingTicket);
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
