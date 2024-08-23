using EventBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.GroupUserManagement.Models
{
    public class GroupUserResponse
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string UserId { get; set; }
    }
}
