using Microsoft.AspNetCore.Http;

namespace Shared.DataTransferObjects.User;
public class UpdateUserDto
{
    public string About { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IFormFile Image { get; set; }
}
