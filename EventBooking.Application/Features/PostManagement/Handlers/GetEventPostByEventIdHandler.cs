using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Features.GroupManagement.Models;
using EventBooking.Application.Features.GroupManagement.Queries;
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

namespace EventBooking.Application.Features.PostManagement.Handlers
{
    public class GetEventPostByEventIdHandler : IRequestHandler<GetEventPostByEventIdQuery, IEnumerable<EventPostResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventPostByEventIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventPostResponse>> Handle(GetEventPostByEventIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var eventPosts= await _unitOfWork.EventPostRepository.GetEventPostsByEventIdAsync(request.EventId);
                if (eventPosts == null || !eventPosts.Any())
                    throw new ErrorException(StatusCodes.Status204NoContent, ResponseCodeConstants.NOT_FOUND, "Không có bài viết ");

                return _mapper.Map<IEnumerable<EventPostResponse>>(eventPosts);

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
