using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Common.Services.Interfaces
{
    public interface IEmailService
    {
        // Phương thức không đồng bộ để gửi email đến danh sách người nhận
        // Trả về true nếu gửi thành công, false nếu thất bại
        Task<bool> SendEmailAsync(IEnumerable<string> toList, string subject, string body);
    }
}
