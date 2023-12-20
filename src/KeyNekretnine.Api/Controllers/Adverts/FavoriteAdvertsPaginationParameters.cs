using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.Adverts;
public record FavoriteAdvertsPaginationParameters : RequestParameters
{
    public FavoriteAdvertsPaginationParameters() => OrderBy = "createdFavoriteDate";
}