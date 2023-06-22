using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;

namespace Application.Core.Adverts.Queries.GetMyAdvertById;
public sealed record GetMyAdvertByIdQuery(int AdvertId, string Email) : IQuery<MyAdvertDto>;