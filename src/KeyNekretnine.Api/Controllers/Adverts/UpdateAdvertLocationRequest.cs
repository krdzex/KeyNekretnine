namespace KeyNekretnine.Api.Controllers.Adverts;
public sealed record UpdateAdvertLocationRequest(
    string Address,
    double? Longitude,
    double? Latitude,
    int? NeighborhoodId);