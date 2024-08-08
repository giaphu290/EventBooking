using EventBooking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserManage.Queries
{
    public class GetUserByHoVaTenQuery : IRequest<IEnumerable<User>>
    {

        public string HoVaTen { get; set; }

        public GetUserByHoVaTenQuery(string hoVaTen)
        {
            HoVaTen = hoVaTen;
        }
    }
}
