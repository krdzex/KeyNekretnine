using KeyNekretnine.Application.Abstraction.Email;
using KeyNekretnine.Application.Core.Adverts.Commands.ApproveAdvert;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace KeyNekretnine.Infrastructure.EmailProvider;
internal sealed class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ISendGridClient _sendGridClient;
    public EmailService(IConfiguration configuration, ISendGridClient sendGridClient)
    {
        _configuration = configuration;
        _sendGridClient = sendGridClient;
    }

    public async Task<bool> SendEmailConfrim(string email, string token, CancellationToken cancellationToken)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;
        var templateId = sendGridConfigSection.GetSection("ConfirmEmailTemplateId").Value;

        var confirmationlink = "https://keynekretnineapi-latest.onrender.com/api/user/confirm-email?token=" + token + "&email=" + email;

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
        };

        msg.SetTemplateId(templateId);
        msg.SetTemplateData(new { verifyEmailUrl = confirmationlink });
        msg.AddTo(email);

        var response = await _sendGridClient.SendEmailAsync(msg, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendWelcomeEmail(string email, CancellationToken cancellationToken)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = "Welcome",
            PlainTextContent = "Welcome email"
        };

        msg.AddTo(email);

        var response = await _sendGridClient.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendSaleApproveAdvertEmail(ApproveSendEmailInfo emailSendInfo, CancellationToken cancellationToken)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;
        var templateId = sendGridConfigSection.GetSection("SaleAdvertApprovedTemplateId").Value;

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
        };

        msg.SetTemplateId(templateId);

        msg.SetTemplateData(new
        {
            cityNeigh = emailSendInfo.CityAndNeighborhood,
            address = emailSendInfo.Address,
            noOfBedrooms = emailSendInfo.NoOfBedrooms,
            noOfBathrooms = emailSendInfo.NoOfBathrooms,
            sqft = emailSendInfo.FloorSpace + "㎡",
            advertCoverImg = emailSendInfo.CoverImageUrl,
            referenceId = emailSendInfo.ReferenceId,
        });

        msg.AddTo(emailSendInfo.CreatorEmail);

        var response = await _sendGridClient.SendEmailAsync(msg, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendRentApproveAdvertEmail(ApproveSendEmailInfo emailSendInfo, CancellationToken cancellationToken)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;
        var templateId = sendGridConfigSection.GetSection("RentAdvertApprovedTemplateId").Value;

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
        };

        msg.SetTemplateId(templateId);

        msg.SetTemplateData(new
        {
            cityNeigh = emailSendInfo.CityAndNeighborhood,
            address = emailSendInfo.Address,
            noOfBedrooms = emailSendInfo.NoOfBedrooms,
            noOfBathrooms = emailSendInfo.NoOfBathrooms,
            sqft = emailSendInfo.FloorSpace + "㎡",
            advertCoverImg = emailSendInfo.CoverImageUrl,
            referenceId = emailSendInfo.ReferenceId,
        });

        msg.AddTo(emailSendInfo.CreatorEmail);

        var response = await _sendGridClient.SendEmailAsync(msg, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendDailyRentApproveAdvertEmail(ApproveSendEmailInfo emailSendInfo, CancellationToken cancellationToken)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;
        var templateId = sendGridConfigSection.GetSection("DailyRentalAdvertApprovedTemplateId").Value;

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
        };

        msg.SetTemplateId(templateId);

        msg.SetTemplateData(new
        {
            cityNeigh = emailSendInfo.CityAndNeighborhood,
            address = emailSendInfo.Address,
            noOfBedrooms = emailSendInfo.NoOfBedrooms,
            noOfBathrooms = emailSendInfo.NoOfBathrooms,
            sqft = emailSendInfo.FloorSpace + "㎡",
            advertCoverImg = emailSendInfo.CoverImageUrl,
            referenceId = emailSendInfo.ReferenceId,
        });

        msg.AddTo(emailSendInfo.CreatorEmail);

        var response = await _sendGridClient.SendEmailAsync(msg, cancellationToken);
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> SendRejectAdvertEmail(string email, CancellationToken cancellationToken)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;
        var templateId = sendGridConfigSection.GetSection("AdvertRejected").Value;

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
        };

        msg.SetTemplateId(templateId);

        msg.AddTo(email);

        var response = await _sendGridClient.SendEmailAsync(msg, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendUserBanEmail(string email, DateTime? banEnd, CancellationToken cancellationToken)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = $"You are banned",
            PlainTextContent = $"Your account is banned until {banEnd}"
        };

        msg.AddTo(email);

        var response = await _sendGridClient.SendEmailAsync(msg, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendUserUnbanEmail(string email, CancellationToken cancellationToken)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = $"You are unbanned",
            PlainTextContent = $"Your account is unbanned"
        };

        msg.AddTo(email);

        var response = await _sendGridClient.SendEmailAsync(msg, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendResetPasswordLink(string email, string token, CancellationToken cancellationToken)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;

        var resetPasswordLink = "http://localhost:3000/forgot-password?token=" + token + "&email=" + email;

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = "Reset your password",
            PlainTextContent = resetPasswordLink
        };

        msg.AddTo(email);

        var response = await _sendGridClient.SendEmailAsync(msg, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendMessageToAdvertOwner(
        string advertOwnerEmail,
        string senderFullName,
        string senderPhoneNumber,
        string senderEmailAddress,
        string stringsenderMessage,
        CancellationToken cancellationToken)
    {
        var sendGridConfigSection = _configuration.GetSection("SendGridEmailSettings");

        var fromEmail = sendGridConfigSection.GetSection("FromEmail").Value;
        var fromName = sendGridConfigSection.GetSection("FromName").Value;

        var msg = new SendGridMessage
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = $"{senderFullName} vam salje poruku",
            PlainTextContent = $"{senderFullName}, ${senderEmailAddress}, ${senderPhoneNumber}. Poruka ${stringsenderMessage}"
        };

        msg.AddTo(advertOwnerEmail);

        var response = await _sendGridClient.SendEmailAsync(msg, cancellationToken);
        return response.IsSuccessStatusCode;
    }
}