namespace Entities.Models;
public class Agent : EntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string TwitterUrl { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string LinkedinUrl { get; set; }
    public IEnumerable<Advert> Adverts { get; set; }

    public Agency Agency { get; set; }
    public int AgencyId { get; set; }

    public IEnumerable<AgentLanguage> AgentLanguages { get; set; }

}