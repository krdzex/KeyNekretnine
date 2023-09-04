using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAdverts;
public sealed record GetAgencyAdvertsQuery(int AgencyId) : IQuery<IReadOnlyList<AgencyAdvertResponse>>;