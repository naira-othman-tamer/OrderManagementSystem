
namespace OrderManagementSystem.Application.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendOrderStatusEmailAsync(OrderStatusEmailDto emailDto)
        {
            var subject = $"Order #{emailDto.OrderId} Status Update";
            var body = $@"
                <h2>Hello {emailDto.CustomerName},</h2>
                <p>Your order <strong>#{emailDto.OrderId}</strong> status has been updated to: <strong>{emailDto.NewStatus}</strong></p>
                <p>Thank you for shopping with us!</p>
                <br/>
                <p>Best regards,<br/>Order Management Team</p>
            ";

            using var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = _emailSettings.EnableSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(emailDto.CustomerEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
