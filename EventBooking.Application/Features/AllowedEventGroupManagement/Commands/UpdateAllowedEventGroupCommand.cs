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
    public class UpdateAllowedEventGroupValidator : AbstractValidator<UpdateAllowedEventGroupCommand>
    {
        public UpdateAllowedEventGroupValidator() {

            RuleFor(m => m.Id)
              .NotEmpty().WithMessage("Id không được để trống!")
              .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");


        }
    }





    public class UpdateAllowedEventGroupCommand : IRequest<AllowedEventGroupResponse>
    {
        public int Id { get; set; }
        public int? EventId { get; set; }
        public int? GroupId { get; set; }
    }
}
