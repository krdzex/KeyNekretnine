namespace KeyNekretnine.Application.Core.Cities.Queries.GetMostPopularCities;
public sealed class PopularCityReponse
{
    public int Id { get; set; }
    public int AdvertsCount { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
}
