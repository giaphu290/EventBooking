using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.PostManagement.Commands;
using EventBooking.Application.Features.TicketManagement.Commands;
using EventBooking.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.TicketManagement.Handlers
{
    public class DeleteEventTicketHandler : IRequestHandler<DeleteEventTicketCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;
        public DeleteEventTicketHandler(IUnitOfWork unitOfWork, IUserContextService userContextService, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<bool> Handle(DeleteEventTicketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existEvent = await _unitOfWork.EventTicketRepository.GetByIdAsync(request.Id);
                if (existEvent == null || existEvent.IsDelete)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy vé");
                }

                string currentUserId = _userContextService.GetCurrentUserId();

                existEvent.DeletedBy = currentUserId;
                existEvent.DeletedTime = _timeService.SystemTimeNow;
                existEvent.IsActive = false;
                existEvent.IsDelete = true;

                await _unitOfWork.SaveChangeAsync();
                return true;
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
