using AutoMapper;
using EventBooking.Application.Common.Constants;
using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.Auth.LoginManage.Models;
using EventBooking.Application.Features.Auth.LoginManage.Queries;
using EventBooking.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.LoginManage.Handlers
{
    internal class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, GetCurrentUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public GetCurrentUserHandler(IUserContextService userContextService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userContextService = userContextService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetCurrentUserResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var currentUserId = _userContextService.GetCurrentUserId();
                var curentUser = await _unitOfWork.UserRepository.GetByIdAsync(currentUserId);
                return _mapper.Map<GetCurrentUserResponse>(curentUser);
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
