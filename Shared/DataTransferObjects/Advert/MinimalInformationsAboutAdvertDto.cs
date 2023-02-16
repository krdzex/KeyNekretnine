namespace Shared.DataTransferObjects.Advert;
public class MinimalInformationsAboutAdvertDto
{
    public int Id { get; set; }
    public string Location { get; set; }
    public double Price { get; set; }
    public string Cover_Image_Url { get; set; }
    public DateTime Created_Date { get; set; }
    public int No_Of_Bathrooms { get; set; }
    public int No_Of_Bedrooms { get; set; }
    public double Floor_Space { get; set; }
    private string Purpose_Name_Sr { get; set; }
    private string Purpose_Name_En { get; set; }
    public DifferentLanguagesDto Purpose_Name { get { return new DifferentLanguagesDto { Sr = Purpose_Name_Sr, En = Purpose_Name_En }; } }
    public string Street { get; set; }
}