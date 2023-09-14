using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertForAdminByReferenceId;
public sealed record GetAdvertForAdminByReferenceIdQuery(string ReferenceId) : IQuery<AdvertForAdminResponse>;