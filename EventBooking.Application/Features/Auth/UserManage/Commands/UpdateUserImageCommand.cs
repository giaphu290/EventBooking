using EventBooking.Application.Features.Auth.UserManage.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserManage.Commands
{
    public class UpdateUserImageCommand : IRequest<UpdateUserImageResponse>
    {
        [Required(ErrorMessage = "Chưa chọn User để cập nhật Image!")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "File không được bỏ trống!")]
        public IFormFile File { get; set; }
    }
}
