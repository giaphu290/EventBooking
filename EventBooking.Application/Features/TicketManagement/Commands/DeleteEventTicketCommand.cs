using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.TicketManagement.Commands
{

    public class DeleteEventTicketValidator : AbstractValidator<DeleteEventTicketCommand>
    {
        public DeleteEventTicketValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty().WithMessage("Chưa chọn vé để xóa!")
                .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");
        }
    }

    public class DeleteEventTicketCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
