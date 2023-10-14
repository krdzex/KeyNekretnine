namespace KeyNekretnine.Domain.UserAdvertFavorites;
public class UserAdvertFavorite
{
    public string UserId { get; set; }
    public Guid AdvertId { get; set; }
    public DateTime CreatedFavoriteDate { get; set; }
}