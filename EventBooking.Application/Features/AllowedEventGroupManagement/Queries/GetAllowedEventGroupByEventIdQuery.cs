using EventBooking.Application.Features.AllowedEventGroupManagement.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.AllowedEventGroupManagement.Queries
{
    public class GetAllowedEventGroupByEventIdValidator : AbstractValidator<GetAllowedEventGroupByEventIdQuery>
    {
        public GetAllowedEventGroupByEventIdValidator()
        {
            RuleFor(m => m.EventId).GreaterThan(0).WithMessage("Yêu cầu nhập event Id");
        }
    }

    public class GetAllowedEventGroupByEventIdQuery : IRequest<IEnumerable<AllowedEventGroupResponse>>
    {
        public int EventId { get; set; }
    }
}
