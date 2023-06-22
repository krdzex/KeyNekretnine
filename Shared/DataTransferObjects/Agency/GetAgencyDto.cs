using Shared.DataTransferObjects.Language;

namespace Shared.DataTransferObjects.Agency;
public class GetAgencyDto
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string Website_Url { get; set; }
    public TimeSpan Work_Start_Time { get; set; }
    public TimeSpan Work_End_Time { get; set; }
    public string Twitter_Url { get; set; }
    public string Facebook_Url { get; set; }
    public string Instagram_Url { get; set; }
    public string Linkedin_Url { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public IEnumerable<LanguageDto> Languages { get; set; }
}