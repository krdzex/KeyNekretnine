using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetMyAdvertByReferenceId;
public sealed record GetMyAdvertByReferenceIdQuery(string ReferenceId, string UserId) : IQuery<MyAdvertResponse>;