using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAdverts;
public sealed record GetAgencyAdvertsQuery(Guid AgencyId) : IQuery<IReadOnlyList<AgencyAdvertResponse>>;