using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetImageUpdate;
public sealed record GetImageUpdateQuery(Guid UpdateId) : IQuery<ImagesUpdateResponse>;