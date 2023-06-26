namespace Entities.Models;
public class Agency : EntityBase
{
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string WebsiteUrl { get; set; }
    public string ImageUrl { get; set; }
    public TimeOnly? WorkStartTime { get; set; }
    public TimeOnly? WorkEndTime { get; set; }
    public string TwitterUrl { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string LinkedinUrl { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public IEnumerable<AgencyLanguage> AgencyLanguages { get; set; }

    public User User { get; set; }
    public string UserId { get; set; }

    public IEnumerable<ImaginaryAgent> ImaginaryAgents { get; set; }
}
