using KeyNekretnine.Application.Abstraction.Email;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace KeyNekretnine.Infrastructure.EmailProvider;
internal sealed class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> SendEmailConfrim(string email, string token, CancellationToken cancellationToken)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;
        var templateId = sendGridConfigSection.GetSection("EmailTemplateId").Value;

        var confirmationlink = "http://localhost:8080/api/user/confirm-email?token=" + token + "&email=" + email;

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

    public async Task<bool> SendWelcomeEmail(string email, CancellationToken cancellationToken)
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

    public async Task<bool> SendApproveAdvertEmail(string email, string referenceId, CancellationToken cancellationToken)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;

        var client = new SendGridClient(Environment.GetEnvironmentVariable("SEND_GRID_API_KEY"));

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = $"Approved advert",
            PlainTextContent = $"Your advert with reference id {referenceId} is approved from admin"
        };

        msg.AddTo(email);

        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendDeclineAdvertEmail(string email, string referenceId, CancellationToken cancellationToken)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;

        var client = new SendGridClient(Environment.GetEnvironmentVariable("SEND_GRID_API_KEY"));

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = $"Rejected advert {referenceId}",
            PlainTextContent = $"Your advert with reference id {referenceId} is rejected from admin"
        };

        msg.AddTo(email);

        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendUserBanEmail(string email, DateTime? banEnd, CancellationToken cancellationToken)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;

        var client = new SendGridClient(Environment.GetEnvironmentVariable("SEND_GRID_API_KEY"));

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = $"You are banned",
            PlainTextContent = $"Your account is banned until {banEnd}"
        };

        msg.AddTo(email);

        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendUserUnbanEmail(string email, CancellationToken cancellationToken)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;

        var client = new SendGridClient(Environment.GetEnvironmentVariable("SEND_GRID_API_KEY"));

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = $"You are unbanned",
            PlainTextContent = $"Your account is unbanned"
        };

        msg.AddTo(email);

        var response = await client.SendEmailAsync(msg, cancellationToken);
        return response.IsSuccessStatusCode;
    }
}