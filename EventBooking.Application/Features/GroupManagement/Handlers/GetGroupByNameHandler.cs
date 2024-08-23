using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.EventManagement.Models;
using EventBooking.Application.Features.EventManagement.Queries;
using EventBooking.Application.Features.GroupManagement.Models;
using EventBooking.Application.Features.GroupManagement.Queries;
using EventBooking.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.GroupManagement.Handlers
{
    public class GetGroupByNameHandler : IRequestHandler<GetGroupByNameQuery, IEnumerable<GroupResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INormalizeVietnamese _normalizeVietnamese;

        public GetGroupByNameHandler(IMapper mapper, IUnitOfWork unitOfWork, INormalizeVietnamese normalizeVietnamese)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _normalizeVietnamese = normalizeVietnamese;
        }
        public async Task<IEnumerable<GroupResponse>> Handle(GetGroupByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string names = _normalizeVietnamese.NormalizeVietnamese(request.Name);
                var groups = await _unitOfWork.GroupRepository.GetGroupsByNameAsync(names);
                if (groups == null || !groups.Any())
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy sự kiện");
                }

                return _mapper.Map<IEnumerable<GroupResponse>>(groups);
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
