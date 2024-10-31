using KeyNekretnine.Application.Core.Adverts.Commands.ApproveAdvert;

namespace KeyNekretnine.Application.Abstraction.Email;
public interface IEmailService
{
    Task<bool> SendEmailConfrim(string email, string token, CancellationToken cancellationToken);
    Task<bool> SendWelcomeEmail(string email, CancellationToken cancellationToken);
    Task<bool> SendSaleApproveAdvertEmail(ApproveSendEmailInfo emailSendInfo, CancellationToken cancellationToken);
    Task<bool> SendRentApproveAdvertEmail(ApproveSendEmailInfo emailSendInfo, CancellationToken cancellationToken);
    Task<bool> SendDailyRentApproveAdvertEmail(ApproveSendEmailInfo emailSendInfo, CancellationToken cancellationToken);
    Task<bool> SendRejectAdvertEmail(string email, CancellationToken cancellationToken);
    Task<bool> SendUserBanEmail(string email, DateTime? banEnd, CancellationToken cancellationToken);
    Task<bool> SendUserUnbanEmail(string email, CancellationToken cancellationToken);
    Task<bool> SendResetPasswordLink(string email, string token, CancellationToken cancellationToken);
    Task<bool> SendMessageToAdvertOwner(string advertOwnerEmail, string senderFullName, string senderPhoneNumber, string senderEmailAddress, string stringsenderMessage, CancellationToken cancellationToken);

}