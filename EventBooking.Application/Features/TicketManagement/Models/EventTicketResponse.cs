using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.TicketManagement.Models
{
    public class EventTicketResponse
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
