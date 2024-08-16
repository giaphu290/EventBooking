using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.EventManagement.Commands
{
    public class DeleteEventValidator : AbstractValidator<DeleteEventCommand>
    {
        public DeleteEventValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty().WithMessage("Chưa chọn sự kiện để xóa!")
                .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");
        }
    }

    public class DeleteEventCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
