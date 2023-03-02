using Entities.Models;
using Shared.RequestFeatures;

namespace Service.Contracts;
public interface ITokenService
{
    Task<string> CreateToken(User user);
    Task<string> CreateRefreshToken(User user);
    Task<TokenRequest> VerifyRefreshToken(TokenRequest request);
    string ValidateJwtToken(string jwtToken);
};