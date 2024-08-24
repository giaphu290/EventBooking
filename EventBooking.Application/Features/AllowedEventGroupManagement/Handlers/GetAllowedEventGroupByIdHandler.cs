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
    public class GetAllowedEventGroupByIdHandler : IRequestHandler<GetAllowedEventGroupByIdQuery, AllowedEventGroupResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllowedEventGroupByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AllowedEventGroupResponse> Handle(GetAllowedEventGroupByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.AllowedEventGroupRepository.GetByIdAsync(request.Id);

                if (result == null || result.IsDelete)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy lời mời nhóm.");
                }

                return _mapper.Map<AllowedEventGroupResponse>(result);
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã có lỗi xảy ra.");
            }
        }
    }
}
