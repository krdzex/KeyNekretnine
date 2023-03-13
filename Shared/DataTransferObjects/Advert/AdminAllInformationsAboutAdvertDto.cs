using Shared.DataTransferObjects.Image;
using System.Text.Json.Serialization;

namespace Shared.DataTransferObjects.Advert;
public class AdminAllInformationsAboutAdvertDto
{
    public int Id { get; set; }
    public double Price { get; set; }
    private string Description_Sr { get; set; }
    private string Description_En { get; set; }

    public DifferentLanguagesDto Description
    {
        get
        {
            if (string.IsNullOrEmpty(Description_Sr) && string.IsNullOrEmpty(Description_En))
            {
                return Description;
            }
            else
            {
                return new DifferentLanguagesDto { Sr = Description_Sr, En = Description_En };
            }

        }
        set
        {
            Description_Sr = value.Sr;
            Description_En = value.En;
        }
    }


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
    public bool Is_Funished { get; set; }
    public DateTime Created_Date { get; set; }
    public int Year_Of_Building_Created { get; set; }
    public int Building_Floor { get; set; }
    private string Purpose_Name_Sr { get; set; }
    private string Purpose_Name_En { get; set; }

    public DifferentLanguagesDto Purpose_Name
    {
        get
        {
            if (string.IsNullOrEmpty(Purpose_Name_Sr) && string.IsNullOrEmpty(Purpose_Name_En))
            {
                return Purpose_Name;
            }
            else
            {
                return new DifferentLanguagesDto { Sr = Purpose_Name_Sr, En = Purpose_Name_En };
            }

        }
        set
        {
            Purpose_Name_Sr = value.Sr;
            Purpose_Name_En = value.En;
        }
    }

    private string Type_Name_Sr { get; set; }
    private string Type_Name_En { get; set; }

    public DifferentLanguagesDto Type_Name
    {
        get
        {
            if (string.IsNullOrEmpty(Type_Name_Sr) && string.IsNullOrEmpty(Type_Name_En))
            {
                return Type_Name;
            }
            else
            {
                return new DifferentLanguagesDto { Sr = Type_Name_Sr, En = Type_Name_En };
            }

        }
        set
        {
            Type_Name_Sr = value.Sr;
            Type_Name_En = value.En;
        }
    }

    public string Creator { get; set; }
    public string Street { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string City_Name { get; set; }
    public int City_Id { get; set; }
    public string Neighborhood_Name { get; set; }

    private string Status_Name_Sr { get; set; }
    private string Status_Name_En { get; set; }

    public DifferentLanguagesDto Status_Name
    {
        get
        {
            if (string.IsNullOrEmpty(Status_Name_Sr) && string.IsNullOrEmpty(Status_Name_En))
            {
                return Status_Name;
            }
            else
            {
                return new DifferentLanguagesDto { Sr = Status_Name_Sr, En = Status_Name_En };
            }

        }
        set
        {
            Status_Name_Sr = value.Sr;
            Status_Name_En = value.En;
        }
    }
}
