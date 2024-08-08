using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserManage.Models
{
    public class GetUserDetailResponse
    {
        public string HoVaTen { get; set; }
        public string Bio { get; set; }
        public string ImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
