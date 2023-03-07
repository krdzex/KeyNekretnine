namespace Service.Contracts;
public interface IEmailService
{
    Task<bool> SendEmailConfrim(string email, string token);
    Task<bool> SendWelcomeEmail(string email);
    Task<bool> SendApproveAdvertEmail(string email, int advertId);
    Task<bool> SendDeclineAdvertEmail(string email, int advertId);
    Task<bool> SendUserBanEmail(string email, DateTime banEnd);
    Task<bool> SendUserUnbanEmail(string email);
}