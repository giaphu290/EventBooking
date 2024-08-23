using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.PostManagement.Commands
{
    public class DeleteEventPostValidator : AbstractValidator<DeleteEventPostCommand>
    {
        public DeleteEventPostValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty().WithMessage("Chưa chọn bài viết để xóa!")
                .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");
        }
    }

    public class DeleteEventPostCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
