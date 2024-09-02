using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.GroupManagement.Commands;
using EventBooking.Application.Features.GroupManagement.Models;
using EventBooking.Application.Features.PostManagement.Models;
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

namespace EventBooking.Application.Features.TicketManagement.Handlers
{
    public class CreateEventTicketHandler : IRequestHandler<CreateEventTicketCommand, EventTicketResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;
        public CreateEventTicketHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;           
            _timeService = timeService;
        }

        public async Task<EventTicketResponse> Handle(CreateEventTicketCommand request, CancellationToken cancellationToken)
        {


            try
            {
                var currentUserId = _userContextService.GetCurrentUserId();

                var events = await _unitOfWork.EventRepository.GetByIdAsync(request.EventId)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy sự kiện");
                if (events.IsDelete || !events.IsActive)
                {
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Sự kiện đã bị xoá hoặc không tìm thấy");
                }
                if(events.RegistrationEndDate  <  _timeService.SystemTimeNow)
                {
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.FAILED, "Đã ngừng bán vé");

                }
                var users = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy người mua vé");
                if (users.IsDelete || !users.IsActive)
                {
                    throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy người mua vé hoặc đã bị xoá");
                }
                if(request.IsPaid == true)
                {
                    request.PurchaseDate = _timeService.SystemTimeNow.DateTime;
                }    
                var newTicket = _mapper.Map<EventTicket>(request);
                newTicket.CreatedBy = currentUserId;
                newTicket.LastUpdatedBy = currentUserId;
                newTicket.CreatedTime = _timeService.SystemTimeNow;
                newTicket.LastUpdatedTime = _timeService.SystemTimeNow;
                newTicket = await _unitOfWork.EventTicketRepository.AddAsync(newTicket);
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<EventTicketResponse>(newTicket);
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
