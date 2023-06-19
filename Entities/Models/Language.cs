namespace Entities.Models;
public class Language : EntityBase
{
    public string Name { get; set; }
    public IEnumerable<AgencyLanguage> AgencyLanguage { get; set; }
}