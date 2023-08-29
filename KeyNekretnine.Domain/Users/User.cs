using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.UserAdvertFavorites;
using KeyNekretnine.Domain.UserAdvertReports;
using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Domain.Users;
public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime AccountCreatedDate { get; set; } = DateTime.Now;
    public string ProfileImageUrl { get; set; }
    public string About { get; set; }
    public List<Advert> Adverts { get; set; }
    public bool IsBanned { get; set; }
    public DateTime? BanEnd { get; set; }
    public IEnumerable<UserAdvertFavorite> UserAdvertFavorites { get; set; }
    public IEnumerable<UserAdvertReport> UserAdvertReports { get; set; }
    public Agency Agency { get; set; }
}