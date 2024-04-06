using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetFeaturesUpdate;
public sealed record GetFeaturesUpdateQuery(Guid UpdateId) : IQuery<FeaturesUpdateResponse>;