using EventBooking.Application.Features.EventManagement.Models;
using EventBooking.Application.Features.GroupManagement.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.GroupManagement.Queries
{
    public class GetGroupByIdValidator : AbstractValidator<GetGroupByIdQuery>
    {
        public GetGroupByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

        }
    }


    public class GetGroupByIdQuery : IRequest<GroupResponse>
    {
        public int Id { get; set; }
    }
}
