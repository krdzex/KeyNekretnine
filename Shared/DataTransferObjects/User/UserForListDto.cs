namespace Shared.DataTransferObjects.User;
public class UserForListDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string User_name { get; set; }
    public bool Is_Banned { get; set; }
    public DateTime Account_Created_Date { get; set; }

}