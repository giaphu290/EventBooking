using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.GroupUserManagement.Commands
{
    public class DeleteGroupUserValidator : AbstractValidator<DeleteGroupUserCommand>
    {
        public DeleteGroupUserValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty().WithMessage("Chưa chọn Id Group User để xóa!")
                .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");
        }
    }

    public class DeleteGroupUserCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
