using EventBooking.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Domain.Entities
{
    public class AllowedEventGroup : BaseEntity
    {
        public int Id { get; set; } 
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
