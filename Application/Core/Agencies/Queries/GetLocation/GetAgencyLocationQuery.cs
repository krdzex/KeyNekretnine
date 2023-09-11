using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetLocation;
public sealed record GetAgencyLocationQuery(Guid AgencyId) : IQuery<AgencyLocationResponse>;
