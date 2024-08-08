using EventBooking.Application.Features.Auth.LoginManage.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.LoginManage.Queries
{
    public class GetCurrentUserQuery : IRequest<GetCurrentUserResponse>
    {
    }
}
