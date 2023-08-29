using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertById;
public sealed record GetAdvertByIdQuery(int Id) : IQuery<AllInfomrationsAboutAdvertDto>;