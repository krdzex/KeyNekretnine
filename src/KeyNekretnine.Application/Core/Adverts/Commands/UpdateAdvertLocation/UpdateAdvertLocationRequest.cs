namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertLocation;
public record UpdateAdvertLocationRequest(
    double? Latitude,
    double? Longitude,
    string Address,
    int NeighborhoodId);