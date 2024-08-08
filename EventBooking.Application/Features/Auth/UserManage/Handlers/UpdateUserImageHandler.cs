using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Application.Features.Auth.UserManage.Commands;
using EventBooking.Application.Features.Auth.UserManage.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserManage.Handlers
{
    public class UpdateUserImageHandler : IRequestHandler<UpdateUserImageCommand, UpdateUserImageResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public UpdateUserImageHandler(IFileService fileService, IUnitOfWork unitOfWork)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateUserImageResponse> Handle(UpdateUserImageCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return new UpdateUserImageResponse { IsSuccess = false };
            }

            try
            {
                // Kiểm tra ảnh của người dùng đã tồn tại hay chưa. nếu tồn tại thì xoá nó đi 
                if (!string.IsNullOrEmpty(user.ImagePath))
                {
                    var imageName = user.ImagePath.Split('/').Last();

                    await _fileService.DeleteImageAsync(imageName, "images");
                }
                // Tải lên ảnh mới và nhận về đường dẫn của ảnh
                var imageUrl = await _fileService.UploadImageAsync(request.File, "images");
                user.ImagePath = "/images/" + imageUrl;
                // Cập nhật thông tin người dùng
                await _unitOfWork.UserRepository.UpdateUserAsync(user);

                return new UpdateUserImageResponse
                {
                    IsSuccess = true,
                    ImageUrl = user.ImagePath
                };
            }
            catch (Exception)
            {
                return new UpdateUserImageResponse { IsSuccess = false };
            }
        }
    }
}
