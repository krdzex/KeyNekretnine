using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Users;

namespace KeyNekretnine.Domain.UserAdvertFavorites;
public class UserAdvertFavorite
{
    public string UserId { get; set; }
    public User User { get; set; }

    public int AdvertId { get; set; }
    public Advert Advert { get; set; }

    public DateTime CreatedFavoriteDate { get; set; }
}