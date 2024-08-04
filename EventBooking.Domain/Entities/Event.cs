using EventBooking.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Domain.Entities
{
    public class Event : BaseEntity
    {
        public int Id { get; set; }
        public decimal Price { get; set; } = 0;
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? RegistrationEndDate { get; set; }
        public bool IsPrivate { get; set; }
        public EventStatus Status { get; set; }
        public int Capacity { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<EventPost> EventPosts { get; set; }
        public ICollection<EventTicket> EventTickets { get; set; }
        public ICollection<EventInvitation> EventInvitations { get; set; }
        public ICollection<AllowedEventGroup> AllowedEventGroups { get; set; }
    }
}
