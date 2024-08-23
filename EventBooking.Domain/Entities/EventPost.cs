using EventBooking.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Domain.Entities
{
    public class EventPost : BaseEntity
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public string OwnerId { get; set; }
        public User User { get; set; }
    }
}
