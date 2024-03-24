using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetBasicUpdate;
public sealed record GetBasicUpdateQuery(Guid UpdateId) : IQuery<BasicUpdateResponse>;