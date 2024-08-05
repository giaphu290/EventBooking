using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserManage.Models
{
    public class CreateUserResponse
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? PhoneNumber { get; set; }
      
    }
}
