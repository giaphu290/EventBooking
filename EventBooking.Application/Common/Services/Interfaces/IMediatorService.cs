using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Common.Services.Interfaces
{
    public interface IMediatorService
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
    }

}
