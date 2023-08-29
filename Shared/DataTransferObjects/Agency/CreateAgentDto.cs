using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.DataTransferObjects.PhoneNumber;

namespace Shared.DataTransferObjects.Agency;
public class CreateAgentDto
{
    public int AgencyId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string TwitterUrl { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string LinkedinUrl { get; set; }

    [BindNever]
    public string PhoneNumber { get; set; }

    public IFormFile Image { get; set; }

    [BindNever]
    public string ImageUrl { get; set; }
    public IEnumerable<int> LanguageId { get; set; }

    public CreateNumberDto NumberMaker { get; set; }
}