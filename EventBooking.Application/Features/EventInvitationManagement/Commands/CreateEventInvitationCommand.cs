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
    public class CreateEventInvitationValidation : AbstractValidator<CreateEventInvitationCommand>
    {
        public CreateEventInvitationValidation() {
            RuleFor(a => a.EventId)
                .NotEmpty().WithMessage("EventId được yêu cầu.")
                .GreaterThan(0).WithMessage("EventId must be greater than 0.");

            RuleFor(a => a.UserId)
                .NotEmpty().WithMessage("UserId được yêu cầu.")
                .Length(1, 50).WithMessage("UserId must be between 1 and 50 characters.");
            RuleFor(a => a.SentDate)
                .NotEmpty().WithMessage("SentDate is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("SentDate cannot be in the future.");



        }
    }
    public class CreateEventInvitationCommand : IRequest<EventInvitationResponse>
    {
        public int EventId { get; set; }
        public string UserId { get; set; }
        public string? TextResponse { get; set; }
        public DateTime? ResponseDate { get; set; }
        public InvitationResponseType? ResponseType { get; set; }
        public DateTime SentDate { get; set; }
    }

}