using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.PostManagement.Models
{
    public class EventPostResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int EventId { get; set; }
    }
}

