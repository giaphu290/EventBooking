using EventBooking.Application.Common.Services.Interfaces;
using EventBooking.Domain.BaseException;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;


namespace EventBooking.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _senderEmail;
        private readonly string _password;
        private readonly string _host;
        private readonly int _port;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _senderEmail = configuration["EmailSettings:Sender"]
                ?? throw new ErrorException(StatusCodes.Status404NotFound, "Email Sender is not configured.");
            _password = configuration["EmailSettings:Password"];
            _host = configuration["EmailSettings:Host"];
            _port = int.Parse(configuration["EmailSettings:Port"]
                ?? throw new ErrorException(StatusCodes.Status404NotFound, "Email port is not configured."));

            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(IEnumerable<string> toList, string subject, string body)
        {
            try
            {
                // Tạo một đối tượng MimeMessage để xây dựng email

                var emailMessage = new MimeMessage();
                // Thiết lập địa chỉ email người gửi
                emailMessage.From.Add(new MailboxAddress("Your Name", _senderEmail));
                // Thêm từng người nhận vào email

                foreach (var to in toList)
                {
                    emailMessage.To.Add(new MailboxAddress("", to));
                }
                // Thiết lập tiêu đề và nội dung email
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart("html")
                {
                    Text = body // Thiết lập nội dung email là HTML
                };
                // Sử dụng SmtpClient để kết nối và gửi email

                using (var client = new SmtpClient())
                {
                    // Kết nối đến máy chủ SMTP với tùy chọn mã hóa TLS

                    await client.ConnectAsync(_host, _port, SecureSocketOptions.StartTls);
                    // Xác thực với máy chủ SMTP bằng email và mật khẩu

                    await client.AuthenticateAsync(_senderEmail, _password);
                    // Gửi email

                    await client.SendAsync(emailMessage);
                    // Ngắt kết nối khỏi máy chủ SMTP

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email.");
                return false;
            }

            return true;
        }
    }
}
