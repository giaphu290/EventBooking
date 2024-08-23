﻿using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Features.PostManagement.Models;
using EventBooking.Application.Features.PostManagement.Queries;
using EventBooking.Application.Features.TicketManagement.Models;
using EventBooking.Application.Features.TicketManagement.Queries;
using EventBooking.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.TicketManagement.Handlers
{
    public class GetEventTicketByEventIdHandler : IRequestHandler<GetEventTicketByEventIdQuery, IEnumerable<EventTicketResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventTicketByEventIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventTicketResponse>> Handle(GetEventTicketByEventIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var eventTickets = await _unitOfWork.EventTicketRepository.GetEventsByEventIdAsync(request.EventId);
                if (eventTickets == null || !eventTickets.Any())
                    throw new ErrorException(StatusCodes.Status204NoContent, ResponseCodeConstants.NOT_FOUND, "Không có vé");

                return _mapper.Map<IEnumerable<EventTicketResponse>>(eventTickets);

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
