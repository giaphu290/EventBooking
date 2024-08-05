using EventBooking.Application.Common.Constants;
using EventBooking.Application.Features.Auth.UserManage.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserManage.Commands
{
    public class UpdateUserCommand : IRequest<CreateUserResponse>
    {
        [Required(ErrorMessage = "Chưa chọn User để Update!")]
        public required string Id { get; set; }

        [MinLength(2, ErrorMessage = "Họ và Tên phải chứa ít nhất 2 ký tự.")]
        public string? HoVaTen { get; set; }
        public string? Bio { get; set; }

        [EmailAddress(ErrorMessage = "Sai định dạng Email.")]
        public string? Email { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại cần có 10 chữ số.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Tên Role không được bỏ trống!")]
        [JsonIgnore]
        public string? RoleName { get; set; }
    }
}
