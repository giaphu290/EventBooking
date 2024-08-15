using EventBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.EventManagement.Models
{
    public class CreateEventResponse
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Location { get; set; }
        public bool IsPrivate { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RegistrationEndDate { get; set; }
        public EventStatus Status { get; set; }
        public int Capacity { get; set; }
        public string CreateBy { get; set; }

        public string LastUpdatedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }

        public DateTimeOffset LastUpdatedTime { get; set; }
        public string Errors { get; set; }

    }
}
