using EventBooking.Application.Features.TicketManagement.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.TicketManagement.Commands
{
    public class UpdateEventTicketValidator : AbstractValidator<UpdateEventTicketCommand>
    {
        public UpdateEventTicketValidator()
        {
            RuleFor(m => m.Id)
    .NotEmpty().WithMessage("Id không được để trống!");
            RuleFor(a => a.EventId)
                .NotEmpty().WithMessage("EventId is required.")
                .GreaterThan(0).WithMessage("EventId must be greater than 0.")
                .When(a => a.EventId.HasValue);

            RuleFor(a => a.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .Length(1, 50).WithMessage("UserId must be between 1 and 50 characters.")
                .When(a => !string.IsNullOrEmpty(a.UserId));
            RuleFor(t => t.IsPaid)
                .NotNull()
                .WithMessage("IsPaid phải là true or false");
        }
    }

}

public class UpdateEventTicketCommand : IRequest<EventTicketResponse>
{
    public int Id { get; set; }
    public int? EventId { get; set; }
    public string? UserId { get; set; }
    [JsonIgnore]
    public DateTime? PurchaseDate { get; set; }
    public bool? IsPaid { get; set; }
}

