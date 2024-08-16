using EventBooking.Application.Features.EventManagement.Models;
using EventBooking.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.EventManagement.Commands
{
    public class UpdateEventValidator : AbstractValidator<UpdateEventCommand>

    {
        public UpdateEventValidator()
        {
            RuleFor(m => m.Id)
              .NotEmpty().WithMessage("Id không được để trống!");
            RuleFor(command => command.Name)
                .Length(2, 100).WithMessage("Event name must be between 2 and 100 characters.")
                .When(command => !string.IsNullOrEmpty(command.Name));
            RuleFor(command => command.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.")
                .When(command => !string.IsNullOrEmpty(command.Description));
            RuleFor(command => command.Location)
                .NotEmpty().WithMessage("Location is required.")
                .When(command => !string.IsNullOrEmpty(command.Location));
            RuleFor(command => command.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.")
                .When(command => command.Price.HasValue);
            RuleFor(command => command.StartDate)
                .LessThan(command => command.EndDate).WithMessage("Start date must be before the end date.")
                .When(command => command.StartDate != default);
            RuleFor(command => command.EndDate)
                .GreaterThan(command => command.StartDate).WithMessage("End date must be after the start date.")
                .When(command => command.EndDate.HasValue);
            RuleFor(command => command.RegistrationEndDate)
                .LessThanOrEqualTo(command => command.StartDate).WithMessage("Registration end date must be before or on the start date.")
                .When(command => command.RegistrationEndDate.HasValue);
            RuleFor(command => command.IsPrivate)
                .NotNull().WithMessage("Privacy status must be specified.")
                .When(command => command.IsPrivate != default);
            RuleFor(command => command.Status)
                .IsInEnum().WithMessage("Status must be a valid enum value.")
                .When(command => command.Status != default);
            RuleFor(command => command.Capacity)
                .GreaterThanOrEqualTo(1).WithMessage("Capacity must be at least 1.")
                .When(command => command.Capacity.HasValue);
            RuleFor(command => command.HostId)
                .NotEmpty().WithMessage("Host Id is required.")
                .When(command => !string.IsNullOrEmpty(command.HostId));
        }
    }
    public class UpdateEventCommand : IRequest<EventResponse>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public decimal? Price { get; set; } = 0;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? RegistrationEndDate { get; set; }
        public bool? IsPrivate { get; set; }
        public EventStatus? Status { get; set; }
        public int? Capacity { get; set; }
        public string HostId { get; set; }
    }
}
