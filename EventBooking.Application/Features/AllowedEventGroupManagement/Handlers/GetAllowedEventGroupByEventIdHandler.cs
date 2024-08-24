using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Features.AllowedEventGroupManagement.Models;
using EventBooking.Application.Features.AllowedEventGroupManagement.Queries;
using EventBooking.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.AllowedEventGroupManagement.Handlers
{
    public class GetAllowedEventGroupByEventIdHandler : IRequestHandler<GetAllowedEventGroupByEventIdQuery, IEnumerable<AllowedEventGroupResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllowedEventGroupByEventIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AllowedEventGroupResponse>> Handle(GetAllowedEventGroupByEventIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var groupInvitations = await _unitOfWork.AllowedEventGroupRepository.GetAllowedEventGroupsByEventIdAsync(request.EventId)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy lời mời nhóm!");


                if (groupInvitations == null || !groupInvitations.Any())
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy lời mời nhóm nào");
                }

                var result = _mapper.Map<IEnumerable<AllowedEventGroupResponse>>(groupInvitations);
                return result;
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã có lỗi xảy ra");
            }
        }
    }
}
