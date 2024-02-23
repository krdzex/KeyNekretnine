namespace KeyNekretnine.Application.Core.Cities.Queries.GetMostPopularCities;
public sealed class PopularCityReponse
{
    public string Slug { get; init; }
    public int AdvertsCount { get; init; }
    public string Name { get; init; }
    public string ImageUrl { get; init; }
}
