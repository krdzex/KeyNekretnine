using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.RequestFeatures;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencies;
public sealed record GetAgenciesQuery(AgencyParameters AgencyParameters) : IQuery<Pagination<AgencyResponse>>;