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
    public class GetGroupUserByUserIdValidator : AbstractValidator<GetGroupUserByUserIdQuery>
    {
        public GetGroupUserByUserIdValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Yêu cầu UserId");

        }
    }


    public class GetGroupUserByUserIdQuery : IRequest<GroupUserResponse>
    {
        public string UserId { get; set; }
    }
}
