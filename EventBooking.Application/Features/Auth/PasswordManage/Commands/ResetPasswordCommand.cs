using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.PasswordManage.Commands
{
    public class ResetPasswordCommand : IRequest<IdentityResult>
    {
        [Required(ErrorMessage = "Email không được để trống!")]
        [EmailAddress(ErrorMessage = "Sai định dạng Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự.")]
        [MaxLength(20, ErrorMessage = "Mật khẩu không được vượt quá 20 ký tự.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,20}$",
        ErrorMessage = "Mật khẩu phải chứa ít nhất 1 ký tự in hoa, 1 ký tự thường, và 1 ký tự số.")]
        public required string NewPassword { get; set; }

        [Required(ErrorMessage = "Mật khẩu xác nhận không được để trống!")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp với Mật khẩu mới!")]
        public string ConfirmPassword { get; set; }

        public ResetPasswordCommand(string email, string newPassword, string confirmPassword)
        {
            Email = email;
            NewPassword = newPassword;
            ConfirmPassword = confirmPassword;
        }
    }
}
