using EventBooking.Application.Features.PostManagement.Models;
using EventBooking.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.PostManagement.Commands
{
    public class CreateEventPostValidator : AbstractValidator<CreateEventPostCommand>
    {
        public CreateEventPostValidator() {
            RuleFor(a => a.Content)
                .NotEmpty()
                     .WithMessage("Bài viết không được để trống.");
            RuleFor(a => a.EventId)
              .GreaterThan(0)
              .WithMessage("Không có sự kiện liên quan đến bài viết");


        }
    }

    public class CreateEventPostCommand : IRequest<EventPostResponse>
    {
        public string Content { get; set; }
        public int EventId { get; set; }
    
    }
}
