using EventBooking.Application.Features.PostManagement.Models;
using EventBooking.Application.Features.TicketManagement.Models;
using EventBooking.Domain.Entities;
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
    public class CreateEventTicketValidator : AbstractValidator<CreateEventTicketCommand>
    {
        public CreateEventTicketValidator()
        {
            RuleFor(t => t.EventId)
                .NotEmpty()
                 .GreaterThan(0)
                 .WithMessage("Id sự kiện không được để trống");

            // Validate UserId
            RuleFor(t => t.UserId)
                .NotEmpty()
                .WithMessage("Id người dùng không được để trống");

            // Validate IsPaid
            RuleFor(t => t.IsPaid)
                .NotNull()
                .WithMessage("IsPaid phải là true or false");
        }
    }

}

public class CreateEventTicketCommand : IRequest<EventTicketResponse>
{
    public int EventId { get; set; }
    public string UserId { get; set; }
    public bool IsPaid { get; set; }
    [JsonIgnore]
    public DateTime PurchaseDate { get; set; }
}
