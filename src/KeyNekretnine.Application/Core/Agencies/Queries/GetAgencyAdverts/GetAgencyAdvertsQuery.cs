using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyAdverts;
public sealed record GetAgencyAdvertsQuery(Guid AgencyId) : IQuery<IReadOnlyList<AgencyAdvertResponse>>;