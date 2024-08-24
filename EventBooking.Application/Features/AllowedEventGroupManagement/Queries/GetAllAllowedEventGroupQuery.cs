using EventBooking.Application.Features.AllowedEventGroupManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.AllowedEventGroupManagement.Queries
{
    public class GetAllAllowedEventGroupQuery : IRequest<IEnumerable<AllowedEventGroupResponse>>
    {
    }
}
