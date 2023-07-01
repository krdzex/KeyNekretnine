namespace Shared.DataTransferObjects.Agency;
public class GetAgenciesDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Twitter_Url { get; set; }
    public string Facebook_Url { get; set; }
    public string Instagram_Url { get; set; }
    public string Linkedin_Url { get; set; }
    public string Image_Url { get; set; }

    public DateTime Created_Date { get; set; }
    public int Num_Adverts { get; set; }
}