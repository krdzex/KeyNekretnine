using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetMyAdvertById;
public sealed record GetMyAdvertByIdQuery(int AdvertId, string Email) : IQuery<MyAdvertDto>;