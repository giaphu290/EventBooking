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
    public class UpdateGroupValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupValidator()
        {
            RuleFor(m => m.Id)
         .NotEmpty().WithMessage("Id không được để trống!");
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



    public class UpdateGroupCommand : IRequest<GroupResponse>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
