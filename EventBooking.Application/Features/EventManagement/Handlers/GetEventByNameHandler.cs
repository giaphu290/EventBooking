using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.EventManagement.Models;
using EventBooking.Application.Features.EventManagement.Queries;
using EventBooking.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.EventManagement.Handlers
{
    public class GetEventByNameHandler : IRequestHandler<GetEventByNameQuery, IEnumerable<EventResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INormalizeVietnamese _normalizeVietnamese;

        public GetEventByNameHandler(IMapper mapper, IUnitOfWork unitOfWork, INormalizeVietnamese normalizeVietnamese)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _normalizeVietnamese = normalizeVietnamese;
        }
        public async Task<IEnumerable<EventResponse>> Handle(GetEventByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string names = _normalizeVietnamese.NormalizeVietnamese(request.Name);
                var events = await _unitOfWork.EventRepository.GetEventsByNameAsync(names);
                if (!events.Any())
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy sự kiện");
                }

                return _mapper.Map<IEnumerable<EventResponse>>(events);
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
