using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;

namespace Application.Adverts.Queries.GetAdvertById;
public sealed record GetAdvertByIdQuery(int Id) : IQuery<AllInfomrationsAboutAdvertDto>;


