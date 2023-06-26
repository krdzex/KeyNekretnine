using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shared.DataTransferObjects.Agency;
public class CreateAgentDto
{
    public int AgencyId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public IFormFile Image { get; set; }

    [BindNever]
    public string ImageUrl { get; set; }

}