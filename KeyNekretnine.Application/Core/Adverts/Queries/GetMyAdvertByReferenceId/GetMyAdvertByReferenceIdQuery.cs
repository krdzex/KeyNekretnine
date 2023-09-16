using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Adverts.Queries.GetMyAdvertByReferenceId;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetMyAdvertById;
public sealed record GetMyAdvertByReferenceIdQuery(string ReferenceId, string UserId) : IQuery<MyAdvertResponse>;