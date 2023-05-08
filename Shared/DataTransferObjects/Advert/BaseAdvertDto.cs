using System.Text.Json.Serialization;

namespace Shared.DataTransferObjects.Advert;
public abstract class BaseAdvertDto
{
    public int Id { get; set; }

    public DateTime Created_Date { get; set; }

    [JsonIgnore]
    public string Purpose_Name_Sr { get; set; }
    [JsonIgnore]
    public string Purpose_Name_En { get; set; }

    public DifferentLanguagesDto Purpose_Name
    {
        get
        {
            return new DifferentLanguagesDto { Sr = Purpose_Name_Sr, En = Purpose_Name_En };
        }
    }

    [JsonIgnore]
    public string Type_Name_Sr { get; set; }
    [JsonIgnore]
    public string Type_Name_En { get; set; }

    public DifferentLanguagesDto Type_Name
    {
        get
        {
            return new DifferentLanguagesDto { Sr = Type_Name_Sr, En = Type_Name_En };
        }
    }
    public bool Is_Emergency { get; set; }
    public bool Is_Under_Construction { get; set; }
    public bool Is_Furnished { get; set; }
}