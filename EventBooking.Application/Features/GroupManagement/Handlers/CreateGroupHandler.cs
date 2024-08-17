using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.EventManagement.Commands;
using EventBooking.Application.Features.EventManagement.Models;
using EventBooking.Application.Features.GroupManagement.Commands;
using EventBooking.Application.Features.GroupManagement.Models;
using EventBooking.Domain.BaseException;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.GroupManagement.Handlers
{
    public class CreateGroupHandler : IRequestHandler<CreateGroupCommand, GroupResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly INormalizeVietnamese _normalizeVietnamese;
        private readonly ITimeService _timeService;
        public CreateGroupHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, INormalizeVietnamese normalizeVietnamese, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
            _normalizeVietnamese = normalizeVietnamese;
            _timeService = timeService;
        }

        public async Task<GroupResponse> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var currentUserId = _userContextService.GetCurrentUserId();
                var normalizedEventName = _normalizeVietnamese.NormalizeVietnamese(request.Name);
                var existingGroup = _unitOfWork.GroupRepository.GetAllAsync().Result
                    .FirstOrDefault(e => _normalizeVietnamese.NormalizeVietnamese(e.Name)
                    .Equals(normalizedEventName, StringComparison.OrdinalIgnoreCase));
                if (existingGroup != null && !existingGroup.IsDelete)
                {

                    throw new ErrorException(StatusCodes.Status409Conflict, ResponseCodeConstants.DUPLICATE, "Group đã tồn tại");

                }
                if (existingGroup != null && existingGroup.IsDelete)
                {
                    existingGroup.IsActive = true;
                    existingGroup.IsDelete = false;
                    existingGroup.LastUpdatedBy = currentUserId;
                    existingGroup.LastUpdatedTime = _timeService.SystemTimeNow;
                    await _unitOfWork.SaveChangeAsync();
                    return _mapper.Map<GroupResponse>(existingGroup);

                }
                var newGroup = _mapper.Map<Group>(request);
                newGroup.CreatedBy = currentUserId;
                newGroup.LastUpdatedBy = currentUserId;
                newGroup.CreatedTime = _timeService.SystemTimeNow;
                newGroup.LastUpdatedTime = _timeService.SystemTimeNow;
                newGroup = await _unitOfWork.GroupRepository.AddAsync(newGroup);
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<GroupResponse>(newGroup);
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
