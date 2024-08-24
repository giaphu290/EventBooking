using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.AllowedEventGroupManagement.Commands;
using EventBooking.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.AllowedEventGroupManagement.Handlers
{
    public class DeleteAllowedEventGroupHandler : IRequestHandler<DeleteAllowedEventGroupCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;

        public DeleteAllowedEventGroupHandler(IUnitOfWork unitOfWork, IUserContextService userContextService, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<bool> Handle(DeleteAllowedEventGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string currentUserId = _userContextService.GetCurrentUserId();

                var groupInvitations = await _unitOfWork.AllowedEventGroupRepository.GetByIdAsync(request.Id);
                if (groupInvitations == null || groupInvitations.IsDelete)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy lời mời nhóm");
                }

                groupInvitations.DeletedBy = currentUserId;
                groupInvitations.DeletedTime = _timeService.SystemTimeNow;
                groupInvitations.IsActive = false;
                groupInvitations.IsDelete = true;
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
