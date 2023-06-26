using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shared.DataTransferObjects.Agency;
public class UpdateAgencyDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string WebsiteUrl { get; set; }
    public string WorkStartTime { get; set; }
    public string WorkEndTime { get; set; }
    public string TwitterUrl { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string LinkedinUrl { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public IFormFile Image { get; set; }

    [BindNever]
    public string ImageUrl { get; set; }
    public IEnumerable<int> LanguageId { get; set; }
}