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
    public class GetGroupByNameValidator : AbstractValidator<GetGroupByNameQuery>
    {
        public GetGroupByNameValidator()
        {
            RuleFor(x => x.Name).NotNull();

        }
    }


    public class GetGroupByNameQuery : IRequest<IEnumerable<GroupResponse>>
    {
        public string Name { get; set; }
    }
}
