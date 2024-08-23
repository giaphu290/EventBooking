using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.EventInvitationManagement.Commands
{
    public class DeleteEventInvitationValidator : AbstractValidator<DeleteEventInvitationCommand>
    {
        public DeleteEventInvitationValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty().WithMessage("Chưa chọn lời mời để xóa!")
                .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");
        }
    }

    public class DeleteEventInvitationCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
