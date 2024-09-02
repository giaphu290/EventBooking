using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Infrastructure.Persistences.MemoryCache
{
    public class EventMemoryCache
    {
        private readonly IResponseCacheService _cacheService;

        public EventMemoryCache(IResponseCacheService cacheService)
        {
            _cacheService = cacheService;
        }


    }
}
