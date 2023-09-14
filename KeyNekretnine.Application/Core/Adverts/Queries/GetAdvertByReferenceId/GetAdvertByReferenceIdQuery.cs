using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertByReferenceId;
public sealed record GetAdvertByReferenceIdQuery(string ReferenceId) : IQuery<AdvertResponse>;