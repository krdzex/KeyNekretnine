using Microsoft.AspNetCore.Identity;

namespace Entities.Models;
public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime AccountCreatedDate { get; set; } = System.DateTime.Now;
    public List<Advert> Adverts { get; set; }
    public bool IsBanned { get; set; }
    public DateTime? BanEnd { get; set; }
    public IEnumerable<UserAdvertFavorite> UserAdvertFavorites { get; set; }
}