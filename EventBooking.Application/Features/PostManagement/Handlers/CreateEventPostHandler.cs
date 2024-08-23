using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.EventManagement.Commands;
using EventBooking.Application.Features.EventManagement.Models;
using EventBooking.Application.Features.PostManagement.Commands;
using EventBooking.Application.Features.PostManagement.Models;
using EventBooking.Domain.BaseException;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.PostManagement.Handlers
{
    public class CreateEventPostHandler : IRequestHandler<CreateEventPostCommand, EventPostResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;
        public CreateEventPostHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<EventPostResponse> Handle(CreateEventPostCommand request, CancellationToken cancellationToken)
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
                var newPost = _mapper.Map<EventPost>(request);
                newPost.OwnerId = currentUserId;
                newPost.CreatedBy = currentUserId;
                newPost.LastUpdatedBy = currentUserId;
                newPost.CreatedTime = _timeService.SystemTimeNow;
                newPost.LastUpdatedTime = _timeService.SystemTimeNow;
                newPost = await _unitOfWork.EventPostRepository.AddAsync(newPost);
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<EventPostResponse>(newPost);
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
