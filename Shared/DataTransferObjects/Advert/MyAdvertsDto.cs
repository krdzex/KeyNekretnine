using System.Text.Json.Serialization;

namespace Shared.DataTransferObjects.Advert;
public class MyAdvertsDto : BaseAdvertDto
{
    [JsonIgnore]
    public string Description_Sr { get; set; }
    [JsonIgnore]
    public string Description_En { get; set; }

    public DifferentLanguagesDto Description
    {
        get
        {
            return new DifferentLanguagesDto { Sr = Description_Sr, En = Description_En };
        }
    }

    public string Cover_Image_Url { get; set; }
    public string Location { get; set; }

}