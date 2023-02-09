namespace Shared.DataTransferObjects.Advert;
public class MinimalInformationsAboutAdvertDto
{
    public int Id { get; set; }
    public string Location { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string Cover_Image_Url { get; set; }
    public DateTime Created_Date { get; set; }
    public int No_Of_Bathrooms { get; set; }
    public int No_Of_Badrooms { get; set; }
    public double Floor_Space { get; set; }
    public string Purpose_name { get; set; }
    public string Street { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}