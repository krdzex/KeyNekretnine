namespace Entities.Models;
public class ImaginaryAgentLanguage
{
    public int ImaginaryAgentId { get; set; }
    public ImaginaryAgent ImaginaryAgent { get; set; }
    public int LanguageId { get; set; }
    public Language Language { get; set; }
}
