using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Service.Contracts;

namespace Service;
internal sealed class EmailService : IEmailService
{
    private readonly ISendGridClient _sendGridClient;
    private readonly IConfiguration _configuration;

    public EmailService(ISendGridClient sendGridClient, IConfiguration configuration)
    {
        _sendGridClient = sendGridClient;
        _configuration = configuration;
    }

    public async Task<bool> SendEmailConfrim(string email, string token)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;
        var templateId = sendGridConfigSection.GetSection("EmailTemplateId").Value;

        var confirmationlink = "https://localhost:7000/api/user/ConfirmEmail?token=" + token + "&email=" + email;

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = "Confirm your email address"
        };

        msg.SetTemplateId(templateId);
        msg.SetTemplateData(new { verifyEmailUrl = confirmationlink });
        msg.AddTo(email);

        var response = await _sendGridClient.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }

}

