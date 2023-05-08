namespace Shared.DataTransferObjects.Advert;
public class CompareAdvertDto : BaseAdvertDto
{
    public double Price { get; set; }
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
}
