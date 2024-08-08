using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Features.Auth.UserManage.Models;
using EventBooking.Application.Features.Auth.UserManage.Queries;
using EventBooking.Domain.BaseException;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserManage.Handlers
{
    public class GetUserByHoVaTenHandler : IRequestHandler<GetUserByHoVaTenQuery, IEnumerable<User>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByHoVaTenHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<User>> Handle(GetUserByHoVaTenQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetUsersByHoVaTenAsync(request.HoVaTen);
                if (user == null || !user.Any())
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy người dùng");
                }
                return _mapper.Map<IEnumerable<GetAllUserResponse>>(user);
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
