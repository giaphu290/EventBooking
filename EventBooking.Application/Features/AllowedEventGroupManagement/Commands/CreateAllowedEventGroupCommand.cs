using EventBooking.Application.Features.AllowedEventGroupManagement.Models;
using EventBooking.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.AllowedEventGroupManagement.Commands
{
    public class CreateAllowedEventGroupValidator : AbstractValidator<CreateAllowedEventGroupCommand>
    {
        public CreateAllowedEventGroupValidator() { 
        RuleFor(a => a.EventId).NotEmpty().WithMessage("Chưa chọn sự kiện");
        RuleFor(a => a.GroupId).NotEmpty().WithMessage("Chưa chọn nhóm được mời");





        }
    }
    public class CreateAllowedEventGroupCommand : IRequest<AllowedEventGroupResponse>
    {
        public int EventId { get; set; }
        public int GroupId { get; set; }
    }
}
