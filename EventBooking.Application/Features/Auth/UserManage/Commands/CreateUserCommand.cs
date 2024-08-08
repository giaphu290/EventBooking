using EventBooking.Application.Common.Constants;
using EventBooking.Application.Features.Auth.UserManage.Models;
using EventBooking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserManage.Commands
{
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        [Required(ErrorMessage = "Họ và Tên không được bỏ trống!")]
        [MinLength(2, ErrorMessage = "Họ và Tên phải có ít nhất 2 ký tự.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email không được bỏ trống!")]
        [EmailAddress(ErrorMessage = "Sai định dạng Email.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Username không được bỏ trống!")]
        [MinLength(6, ErrorMessage = "Username phải có ít nhất 6 ký tự.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự.")]
        [MaxLength(20, ErrorMessage = "Mật khẩu không được vượt quá 20 ký tự.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,20}$",
         ErrorMessage = "Mật khẩu phải chứa ít nhất 1 ký tự in hoa, 1 ký tự thường, và 1 ký tự số.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được bỏ trống!")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại cần có 10 chữ số.")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Tên Role không được bỏ trống!")]
        public required string RoleName { get; set; }
    }
}