using EventBooking.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Infrastructure.Services
{
    public class TimeService : ITimeService
    {
        public DateTimeOffset SystemTimeNow => DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(7));
    }
}
