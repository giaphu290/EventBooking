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
    public class CreateGroupUserValidator : AbstractValidator<CreateGroupUserCommand>
    {
        public CreateGroupUserValidator()
        {
            RuleFor(command => command.GroupId).NotEmpty().GreaterThan(0).WithMessage("Chưa nhập thông tin Id Group");
            RuleFor(command => command.UserId).NotNull().WithMessage("Chưa nhập Id người dùng");
        }
    }



    public class CreateGroupUserCommand : IRequest<GroupUserResponse>
    {
        public int GroupId { get; set; }
        public string UserId { get; set; }
    }
}
