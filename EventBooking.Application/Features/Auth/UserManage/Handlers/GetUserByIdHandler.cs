using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Features.Auth.UserManage.Models;
using EventBooking.Application.Features.Auth.UserManage.Queries;
using EventBooking.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserManage.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, GetUserDetailResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<GetUserDetailResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
                if (user == null)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy người dùng");
                }

                return new GetUserDetailResponse
                {
                    HoVaTen = user.Name ?? string.Empty,
                    Bio = user.Bio ?? string.Empty,
                    ImageUrl = user.ImagePath ?? string.Empty,
                    PhoneNumber = user.PhoneNumber ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                };
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
