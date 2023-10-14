using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyLocation;
public sealed record GetAgencyLocationQuery(Guid AgencyId) : IQuery<AgencyLocationResponse>;
