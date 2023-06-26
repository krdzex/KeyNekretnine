using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Agency;

namespace Application.Core.Agencies.Queries.GetAgencyLocation;
public sealed record GetAgencyLocationQuery(int AgencyId) : IQuery<AgencyLocationDto>;
