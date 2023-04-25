namespace Shared.DataTransferObjects.User;
public class UserInformationDto
{
    public DateTime Account_Created_Date { get; set; }
    public string Phone_Number { get; set; }
    public string About { get; set; }
    public string Profile_Image_Url { get; set; }
    public string Email { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public bool Email_Confirmed { get; set; }
    public IEnumerable<string> Roles { get; set; }
}