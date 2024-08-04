using EventBooking.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Domain.Entities
{
    public class EventInvitation : BaseEntity
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string? TextResponse { get; set; }
        public DateTime? ResponseDate { get; set; }
        public InvitationResponseType? ResponseType { get; set; }
        public DateTime SentDate { get; set; }
     
    }
}
