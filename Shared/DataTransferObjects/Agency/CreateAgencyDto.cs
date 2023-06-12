using Shared.DataTransferObjects.Auth;

namespace Shared.DataTransferObjects.Agency
{
    public class CreateAgencyDto
    {
        public string Name { get; set; }
        public UserForRegistrationDto AdminUser { get; set; }
    }
}
