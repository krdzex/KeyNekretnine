using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.Adverts;
public class FavoriteAdvertsPaginationParameters : RequestParameters
{
    public FavoriteAdvertsPaginationParameters() => OrderBy = "createdFavoriteDate";
}