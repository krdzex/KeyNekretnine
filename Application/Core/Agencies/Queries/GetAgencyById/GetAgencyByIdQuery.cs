using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Agency;

namespace Application.Core.Agencies.Queries.GetAgencyById;
public sealed record GetAgencyByIdQuery(int AgencyId) : IQuery<GetAgencyDto>;