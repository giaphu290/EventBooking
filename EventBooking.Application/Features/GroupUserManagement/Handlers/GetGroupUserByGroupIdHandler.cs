using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Features.GroupManagement.Models;
using EventBooking.Application.Features.GroupManagement.Queries;
using EventBooking.Application.Features.GroupUserManagement.Models;
using EventBooking.Application.Features.GroupUserManagement.Queries;
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
    public class GetGroupUserByGroupIdHandler : IRequestHandler<GetGroupUserByGroupIdQuery, IEnumerable<GroupUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetGroupUserByGroupIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GroupUserResponse>> Handle(GetGroupUserByGroupIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var groupUsers = await _unitOfWork.GroupUserRepository.GetGroupUserByGroupIdAsync(request.GroupId);
                if (groupUsers == null || !groupUsers.Any())
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy Group User");
                }
                return _mapper.Map<IEnumerable<GroupUserResponse>>(groupUsers);

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
