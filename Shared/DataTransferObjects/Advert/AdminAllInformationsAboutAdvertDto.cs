using Shared.DataTransferObjects.AdvertFeature;
using Shared.DataTransferObjects.Image;
using System.Text.Json.Serialization;

namespace Shared.DataTransferObjects.Advert;
public class AdminAllInformationsAboutAdvertDto : BaseAdvertDto
{
    public double Price { get; set; }

    public IList<ImageDto> Images { get; set; }

    [JsonIgnore]
    public string Cover_Image_Url { get; set; }
    public int No_Of_Bathrooms { get; set; }
    public int No_Of_Bedrooms { get; set; }
    public double Floor_Space { get; set; }
    public bool Has_Elevator { get; set; }
    public bool Has_Garage { get; set; }
    public bool Has_Terrace { get; set; }
    public bool Has_Wifi { get; set; }
    public int Year_Of_Building_Created { get; set; }
    public int Building_Floor { get; set; }

    public string Creator { get; set; }
    public string Street { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string City_Name { get; set; }
    public int City_Id { get; set; }
    public string Neighborhood_Name { get; set; }

    [JsonIgnore]
    public string Status_Name_Sr { get; set; }
    [JsonIgnore]
    public string Status_Name_En { get; set; }

    public DifferentLanguagesDto Status_Name
    {
        get
        {
            return new DifferentLanguagesDto { Sr = Status_Name_Sr, En = Status_Name_En };
        }
    }

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

    public IList<FeatureDto> Features { get; set; }

}
