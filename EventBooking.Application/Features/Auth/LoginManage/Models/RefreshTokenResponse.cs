using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Features.Auth.LoginManage.Models
{
    public class RefreshTokenResponse
    {
        public string VerificationToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
