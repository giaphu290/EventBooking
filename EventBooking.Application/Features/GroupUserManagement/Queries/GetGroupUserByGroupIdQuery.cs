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
    public class GetGroupUserByGroupIdValidator : AbstractValidator<GetGroupUserByGroupIdQuery>
    {
        public GetGroupUserByGroupIdValidator()
        {
            RuleFor(x => x.GroupId).GreaterThan(0);

        }
    }


    public class GetGroupUserByGroupIdQuery : IRequest<IEnumerable<GroupUserResponse>>
    {
        public int GroupId { get; set; }
    }
    
    
}
