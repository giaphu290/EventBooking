using EventBooking.Application.Features.GroupManagement.Models;
using EventBooking.Application.Features.GroupUserManagement.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.GroupUserManagement.Commands
{
    public class UpdateGroupUserValidator : AbstractValidator<UpdateGroupUserCommand>
    {
        public UpdateGroupUserValidator()
        {
            RuleFor(m => m.Id)
         .NotEmpty().WithMessage("Id không được để trống!");
            RuleFor(command => command.GroupId).NotEmpty().GreaterThan(0).WithMessage("Chưa nhập thông tin Id Group");
            RuleFor(command => command.UserId).NotEmpty().WithMessage("Chưa nhập Id người dùng");

        }
    }



    public class UpdateGroupUserCommand : IRequest<GroupUserResponse>
    {
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public string? UserId { get; set; }
    }
}
