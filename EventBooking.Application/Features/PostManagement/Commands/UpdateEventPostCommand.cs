using EventBooking.Application.Features.PostManagement.Models;
using FluentValidation;
using MediatR;

namespace EventBooking.Application.Features.PostManagement.Commands
    {
        public class UpdateEventPostValidator : AbstractValidator<UpdateEventPostCommand>
        {
            public UpdateEventPostValidator()
            {
            RuleFor(m => m.PostId)
        .NotEmpty().WithMessage("Id không được để trống!");
                RuleFor(a => a.EventId)
                  .GreaterThan(0)
                  .WithMessage("Không có sự kiện liên quan đến bài viết");

            }
        }





        public class UpdateEventPostCommand : IRequest<EventPostResponse>
        {
            public int PostId { get; set; }
            public string? Content { get; set; }
            public int? EventId { get; set; }
        }



    }