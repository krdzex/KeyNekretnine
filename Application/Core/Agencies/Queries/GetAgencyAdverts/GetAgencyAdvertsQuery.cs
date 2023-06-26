using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;

namespace Application.Core.Agencies.Queries.GetAgencyAdverts;
public sealed record GetAgencyAdvertsQuery(int AgencyId) : IQuery<List<MinimalInformationsAboutAdvertDto>>;