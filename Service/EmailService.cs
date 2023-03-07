using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Service.Contracts;

namespace Service;
internal sealed class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> SendEmailConfrim(string email, string token)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;
        var templateId = sendGridConfigSection.GetSection("EmailTemplateId").Value;

        var confirmationlink = "https://localhost:7000/api/user/ConfirmEmail?token=" + token + "&email=" + email;

        var client = new SendGridClient(Environment.GetEnvironmentVariable("SEND_GRID_API_KEY"));

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = "Confirm your email address"
        };

        msg.SetTemplateId(templateId);
        msg.SetTemplateData(new { verifyEmailUrl = confirmationlink });
        msg.AddTo(email);

        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendWelcomeEmail(string email)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;

        var client = new SendGridClient(Environment.GetEnvironmentVariable("SEND_GRID_API_KEY"));

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = "Welcome",
            PlainTextContent = "Welcome email"
        };

        msg.AddTo(email);

        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendApproveAdvertEmail(string email, int advertId)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;

        var client = new SendGridClient(Environment.GetEnvironmentVariable("SEND_GRID_API_KEY"));

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = $"Approved advert {advertId}",
            PlainTextContent = $"Your advert with id {advertId} is approved from admin"
        };

        msg.AddTo(email);

        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendDeclineAdvertEmail(string email, int advertId)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;

        var client = new SendGridClient(Environment.GetEnvironmentVariable("SEND_GRID_API_KEY"));

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = $"Declined advert {advertId}",
            PlainTextContent = $"Your advert with id {advertId} is declined from admin"
        };

        msg.AddTo(email);

        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }
}