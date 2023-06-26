using Shared.DataTransferObjects.Language;
using System.Text.Json.Serialization;

namespace Shared.DataTransferObjects.Agency;
public class AllAgentInformationsDto
{
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public string Phone_Number { get; set; }
    public string Image_Url { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string Twitter_Url { get; set; }
    public string Facebook_Url { get; set; }
    public string Instagram_Url { get; set; }
    public string Linkedin_Url { get; set; }
    public IEnumerable<LanguageDto> Languages { get; set; }

    [JsonIgnore]
    public int Agency_Id { get; set; }
    [JsonIgnore]
    public string Agency_Name { get; set; }

    public SimpleAgencyDto Agency
    {
        get
        {
            return new SimpleAgencyDto { Id = Agency_Id, Name = Agency_Name };
        }
    }
}