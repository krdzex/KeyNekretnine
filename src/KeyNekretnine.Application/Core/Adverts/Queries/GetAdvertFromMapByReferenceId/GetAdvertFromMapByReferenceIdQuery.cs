using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertFromMapByReferenceId;
public sealed record GetAdvertFromMapByReferenceIdQuery(string ReferenceId) : IQuery<AdvertFromMapResponse>;