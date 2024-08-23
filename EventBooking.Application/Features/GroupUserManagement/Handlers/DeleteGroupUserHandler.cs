using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.GroupUserManagement.Commands;
using EventBooking.Application.Features.PostManagement.Commands;
using EventBooking.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.GroupUserManagement.Handlers
{
    public class DeleteGroupUserHandler : IRequestHandler<DeleteGroupUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;
        public DeleteGroupUserHandler(IUnitOfWork unitOfWork, IUserContextService userContextService, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<bool> Handle(DeleteGroupUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existGroupUser = await _unitOfWork.GroupUserRepository.GetByIdAsync(request.Id);
                if (existGroupUser == null || existGroupUser.IsDelete)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy Group User");
                }

                string currentUserId = _userContextService.GetCurrentUserId();

                existGroupUser.DeletedBy = currentUserId;
                existGroupUser.DeletedTime = _timeService.SystemTimeNow;
                existGroupUser.IsActive = false;
                existGroupUser.IsDelete = true;

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
