using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Domain.Entities
{
    public enum EventStatus
    {
        Draft,
        OpenToRegistrations,
        ClosedToRegistrations,
        Ongoing,
        Past,
        Cancelled
    }
}
