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
    public class GetAllGroupUserHandler : IRequestHandler<GetAllGroupUserQuery, IEnumerable<GroupUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllGroupUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GroupUserResponse>> Handle(GetAllGroupUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var groups = await _unitOfWork.GroupUserRepository.GetAllAsync();

                if (groups == null || !groups.Any())
                    throw new ErrorException(StatusCodes.Status204NoContent, ResponseCodeConstants.NOT_FOUND, "Không có Group User");

                return _mapper.Map<IEnumerable<GroupUserResponse>>(groups);
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
