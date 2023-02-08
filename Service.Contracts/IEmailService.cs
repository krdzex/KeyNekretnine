namespace Service.Contracts;
public interface IEmailService
{
    Task<bool> SendEmailConfrim(string email, string token);
    Task<bool> SendWelcomeEmail(string email);

}