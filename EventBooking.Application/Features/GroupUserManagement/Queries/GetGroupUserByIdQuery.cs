using EventBooking.Application.Features.GroupManagement.Models;
using EventBooking.Application.Features.GroupUserManagement.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.GroupUserManagement.Queries
{
    public class GetGroupUserByIdValidator : AbstractValidator<GetGroupUserByIdQuery>
    {
        public GetGroupUserByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

        }
    }


    public class GetGroupUserByIdQuery : IRequest<GroupUserResponse>
    {
        public int Id { get; set; }
    }
}
