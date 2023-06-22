using Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Agency;
using Shared.RequestFeatures;

namespace Application.Core.Agencies.Queries.GetAgencies;
public sealed record GetAgenciesQuery(AgencyParameters AgencyParameters) : IQuery<Pagination<GetAgenciesDto>>;