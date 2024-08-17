using EventBooking.Application.Features.GroupManagement.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.GroupManagement.Commands
{
    public class CreateGroupValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupValidator()
        {

            RuleFor(command => command.Name)
           .NotEmpty()
             .WithMessage("Tên Group không được để trống.")
               .Length(2, 50)
                 .WithMessage("Tên Group không được quá 50 ký tự");
            RuleFor(command => command.Description)
          .NotEmpty()
            .WithMessage("Mô tả Group không được để trống.");


        }
    }



    public class CreateGroupCommand : IRequest<GroupResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
