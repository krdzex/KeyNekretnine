using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertLocation;
public sealed record UpdateAdvertLocationCommand(
    string ReferenceId,
    string UserId,
    double Latitude,
    double Longitude,
    string Address,
    int NeighborhoodId,
    bool isAgency) : ICommand;