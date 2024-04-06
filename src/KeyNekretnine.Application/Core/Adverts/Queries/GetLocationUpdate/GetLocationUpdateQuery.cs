using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetLocationUpdate;
public sealed record GetLocationUpdateQuery(Guid UpdateId) : IQuery<LocationUpdateResponse>;