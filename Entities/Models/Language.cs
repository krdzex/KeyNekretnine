namespace Entities.Models;
public class Language : EntityBase
{
    public string Name { get; set; }
    public IEnumerable<AgencyLanguage> AgencyLanguages { get; set; }
    public IEnumerable<AgentLanguage> AgentLanguages { get; set; }
}