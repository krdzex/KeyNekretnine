using Shared.DataTransferObjects.User;

namespace Service.Contracts;
public interface IUserService
{
    Task<UserInformationDto> GetCurrentUserInformations(string email);
}
