using EventBooking.Application.Features.EventManagement.Models;
using EventBooking.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.EventManagement.Commands
{
    public class CreateEventValidator : AbstractValidator<CreateEventCommand>

    {
        public CreateEventValidator() {
            RuleFor(command => command.Name)
                  .NotEmpty()
                     .WithMessage("Event name is required.")
                       .Length(2, 100)
                         .WithMessage("Event name must be between 2 and 100 characters.");

            RuleFor(command => command.Location)
                .NotEmpty()
                .WithMessage("Location is required.");

            RuleFor(command => command.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required.")
                .LessThan(command => command.EndDate)
                .WithMessage("Start date must be before the end date.");

            RuleFor(command => command.EndDate)
                .GreaterThan(command => command.StartDate)
                .WithMessage("End date must be after the start date.");
            RuleFor(command => command.IsPrivate)
                .NotNull()
                .WithMessage("Privacy status must be specified.");
        }
    }
    public class CreateEventCommand : IRequest<CreateEventResponse>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Location { get; set; }
        public decimal? Price { get; set; } = 0;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? RegistrationEndDate { get; set; }
        public bool IsPrivate { get; set; }
        [JsonIgnore]
        public EventStatus Status { get; set; }
        public int? Capacity { get; set; }
    }
}
