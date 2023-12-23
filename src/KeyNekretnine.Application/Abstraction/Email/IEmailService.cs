namespace KeyNekretnine.Application.Abstraction.Email;
public interface IEmailService
{
    Task<bool> SendEmailConfrim(string email, string token, CancellationToken cancellationToken);
    Task<bool> SendWelcomeEmail(string email, CancellationToken cancellationToken);
    Task<bool> SendApproveAdvertEmail(string email, string referenceId, CancellationToken cancellationToken);
    Task<bool> SendDeclineAdvertEmail(string email, string referenceId, CancellationToken cancellationToken);
    Task<bool> SendUserBanEmail(string email, DateTime? banEnd, CancellationToken cancellationToken);
    Task<bool> SendUserUnbanEmail(string email, CancellationToken cancellationToken);
    Task<bool> SendResetPasswordLink(string email, string token, CancellationToken cancellationToken);
    Task<bool> SendMessageToAdvertOwner(string advertOwnerEmail, string senderFullName, string senderPhoneNumber, string senderEmailAddress, string stringsenderMessage, CancellationToken cancellationToken);

}