namespace KeyNekretnine.Application.Core.Auth.Commands.UserLogin;
public sealed class TokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
