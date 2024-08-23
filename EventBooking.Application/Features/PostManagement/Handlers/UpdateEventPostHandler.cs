using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.GroupManagement.Commands;
using EventBooking.Application.Features.GroupManagement.Models;
using EventBooking.Application.Features.PostManagement.Commands;
using EventBooking.Application.Features.PostManagement.Models;
using EventBooking.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.PostManagement.Handlers
{
    public class UpdateEventPostHandler : IRequestHandler<UpdateEventPostCommand, EventPostResponse>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;
        public UpdateEventPostHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
        }
        public async Task<EventPostResponse> Handle(UpdateEventPostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currentUserId = _userContextService.GetCurrentUserId();
                var existingPost = await _unitOfWork.EventPostRepository.GetByIdAsync(request.PostId)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy bài viết");

                if (existingPost.IsDelete || !existingPost.IsActive)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "bài viết không tồn tại");
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
     

                // Validate the host role
                _mapper.Map(request, existingPost);
                existingPost.LastUpdatedBy = currentUserId;
                existingPost.LastUpdatedTime = _timeService.SystemTimeNow;

                await _unitOfWork.SaveChangeAsync();

                return _mapper.Map<EventPostResponse>(existingPost);
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
