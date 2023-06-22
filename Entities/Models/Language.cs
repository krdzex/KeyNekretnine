namespace Entities.Models;
public class Language : EntityBase
{
    public string Name { get; set; }
    public IEnumerable<AgencyLanguage> AgencyLanguages { get; set; }
    public IEnumerable<ImaginaryAgentLanguage> ImaginaryAgentLanguages { get; set; }

}