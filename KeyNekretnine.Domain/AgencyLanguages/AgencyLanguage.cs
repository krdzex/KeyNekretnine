using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Languages;

namespace KeyNekretnine.Domain.AgencyLanguages;
public class AgencyLanguage
{
    public int AgencyId { get; set; }
    public Agency Agency { get; set; }
    public int LanguageId { get; set; }
    public Language Language { get; set; }
}