using EventBooking.Application.Features.GroupManagement.Models;
using EventBooking.Application.Features.PostManagement.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.PostManagement.Queries
{
    public class GetEventPostByIdValidator : AbstractValidator<GetEventPostByIdQuery>
    {
        public GetEventPostByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

        }
    }


    public class GetEventPostByIdQuery : IRequest<EventPostResponse>
    {
        public int Id { get; set; }
    }
}
