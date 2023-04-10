namespace Shared.DataTransferObjects.Advert;
public class MinimalInformationsAboutAdvertDto : BaseAdvertDto
{
    public string Location { get; set; }
    public double Price { get; set; }
    public string Cover_Image_Url { get; set; }
    public int No_Of_Bathrooms { get; set; }
    public int No_Of_Bedrooms { get; set; }
    public double Floor_Space { get; set; }
    public string Street { get; set; }
}