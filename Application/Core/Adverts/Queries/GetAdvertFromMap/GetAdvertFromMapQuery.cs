using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;

namespace Application.Core.Adverts.Queries.GetAdvertFromMap;
public sealed record GetAdvertFromMapQuery(int Id) : IQuery<MinimalInformationsAboutAdvertDto>;