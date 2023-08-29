using KeyNekretnine.Domain.AgencyLanguages;
using KeyNekretnine.Domain.AgentLanguages;
using KeyNekretnine.Domain.Models;

namespace KeyNekretnine.Domain.Languages;
public class Language : EntityBase
{
    public string Name { get; set; }
    public IEnumerable<AgencyLanguage> AgencyLanguages { get; set; }
    public IEnumerable<AgentLanguage> AgentLanguages { get; set; }
}