using EventBooking.Application.Features.Auth.UserManage.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserManage.Queries
{
    public class GetAllUserQuery : IRequest<IEnumerable<GetAllUserResponse>>
    {
    }
}
