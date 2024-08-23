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
    public class GetAllEventPostHandler : IRequestHandler<GetAllEventPostQuery, IEnumerable<EventPostResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllEventPostHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventPostResponse>> Handle(GetAllEventPostQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var eventPosts = await _unitOfWork.EventPostRepository.GetAllAsync();

                    if (eventPosts == null || !eventPosts.Any())
                        throw new ErrorException(StatusCodes.Status204NoContent, ResponseCodeConstants.NOT_FOUND, "Không có bài viết");

                return _mapper.Map<IEnumerable<EventPostResponse>>(eventPosts);
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
