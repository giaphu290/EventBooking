using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Features.EventInvitationManagement.Models;
using EventBooking.Application.Features.EventInvitationManagement.Queries;
using EventBooking.Application.Features.PostManagement.Models;
using EventBooking.Application.Features.PostManagement.Queries;
using EventBooking.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.EventInvitationManagement.Handlers
{
    public class GetEventInvitationByEventIdHandler : IRequestHandler<GetEventInvitationByEventIdQuery, IEnumerable<EventInvitationResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventInvitationByEventIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventInvitationResponse>> Handle(GetEventInvitationByEventIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var eventPosts = await _unitOfWork.EventInvitationRepository.GetEventInvitationsByEventIdAsync(request.EventId);
                if (eventPosts == null || !eventPosts.Any())
                    throw new ErrorException(StatusCodes.Status204NoContent, ResponseCodeConstants.NOT_FOUND, "Không có lời mời trong sự kiện ");

                return _mapper.Map<IEnumerable<EventInvitationResponse>>(eventPosts);

            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã xảy ra lỗi không mong muốn khi lấy dữ liệu.");
            }
        }
    }
}
