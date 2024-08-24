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
    public class GetAllowedEventGroupByIdValdiator : AbstractValidator<GetAllowedEventGroupByIdQuery>
    {
        public GetAllowedEventGroupByIdValdiator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }

    public class GetAllowedEventGroupByIdQuery : IRequest<AllowedEventGroupResponse>
    {
        public int Id { get; set; }
    }
}
