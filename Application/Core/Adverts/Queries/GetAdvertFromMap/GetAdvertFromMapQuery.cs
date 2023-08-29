using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertFromMap;
public sealed record GetAdvertFromMapQuery(int Id) : IQuery<MinimalInformationsAboutAdvertDto>;