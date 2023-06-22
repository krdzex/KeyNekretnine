namespace Entities.Models;
public class AgencyLanguage
{
    public int AgencyId { get; set; }
    public Agency Agency { get; set; }
    public int LanguageId { get; set; }
    public Language Language { get; set; }
}