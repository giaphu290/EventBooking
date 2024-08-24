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
    public class GetAllowedEventGroupByGroupIdValidator : AbstractValidator<GetAllowedEventGroupByGroupIdQuery>
    {
        public GetAllowedEventGroupByGroupIdValidator()
        {
            RuleFor(m => m.GroupId).GreaterThan(0).WithMessage("Yêu cầu nhập group Id");
        }
    }

    public class GetAllowedEventGroupByGroupIdQuery : IRequest<IEnumerable<AllowedEventGroupResponse>>
    {
        public int GroupId { get; set; }
    }
    
    
}
