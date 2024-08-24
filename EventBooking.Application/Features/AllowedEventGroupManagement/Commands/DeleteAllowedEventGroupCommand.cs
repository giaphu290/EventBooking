using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.AllowedEventGroupManagement.Commands
{
    public class DeleteAllowedEventGroupValidator : AbstractValidator<DeleteAllowedEventGroupCommand>
    {
        public DeleteAllowedEventGroupValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty().WithMessage("Chưa chọn lời mời nhóm để xóa.")
                .GreaterThan(0);
        }
    }


    public class DeleteAllowedEventGroupCommand : IRequest<bool>
    {
        public int Id { get; set; }

    }
}
