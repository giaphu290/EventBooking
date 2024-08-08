using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.UserManage.Models
{
    public class UpdateUserImageResponse
    {
        public string ImageUrl { get; set; }
        public bool IsSuccess { get; set; }
    }
}
