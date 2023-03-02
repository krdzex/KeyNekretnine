namespace Shared.DataTransferObjects.Advert;

public class AdminTableAdvertDto
{
    public int Id { get; set; }
    public string Location { get; set; }
    public double Price { get; set; }
    public DateTime Created_Date { get; set; }
    public int No_Of_Bathrooms { get; set; }
    public int No_Of_Bedrooms { get; set; }
    public double Floor_Space { get; set; }
    private string Purpose_Name_Sr { get; set; }
    private string Purpose_Name_En { get; set; }
    private string Status_Name_Sr { get; set; }
    private string Status_Name_En { get; set; }
    public DifferentLanguagesDto Purpose_Name { get { return new DifferentLanguagesDto { Sr = Purpose_Name_Sr, En = Purpose_Name_En }; } }
    public DifferentLanguagesDto Status_Name { get { return new DifferentLanguagesDto { Sr = Status_Name_Sr, En = Status_Name_En }; } }
}