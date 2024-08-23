using EventBooking.Application.Features.EventInvitationManagement.Models;
using EventBooking.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.EventInvitationManagement.Commands
{
    public class UpdateEventInvitationValidation : AbstractValidator<UpdateEventInvitationCommand>
    {
        public UpdateEventInvitationValidation()
        {
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(a => a.EventId)
                .NotEmpty().WithMessage("EventId is required.")
                .GreaterThan(0).WithMessage("EventId must be greater than 0.")
                .When(a => a.EventId.HasValue);

            RuleFor(a => a.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .Length(1, 50).WithMessage("UserId must be between 1 and 50 characters.")
                .When(a => !string.IsNullOrEmpty(a.UserId));

            RuleFor(a => a.TextResponse)
                .MaximumLength(500).WithMessage("TextResponse must not exceed 500 characters.")
                .When(a => !string.IsNullOrEmpty(a.TextResponse));

            RuleFor(a => a.ResponseDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("ResponseDate cannot be in the future.")
                .When(a => a.ResponseDate.HasValue);

            RuleFor(a => a.ResponseType)
                .IsInEnum().WithMessage("ResponseType must be a valid enum value.")
                .When(a => a.ResponseType.HasValue);

        }
    }
    public class UpdateEventInvitationCommand : IRequest<EventInvitationResponse>
    {
        public int Id { get; set; }
        public int? EventId { get; set; }
        public string? UserId { get; set; }
        public string? TextResponse { get; set; }
        public DateTime? ResponseDate { get; set; }
        public InvitationResponseType? ResponseType { get; set; }
    }
}
