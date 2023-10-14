using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Domain.Adverts.Events;
public sealed record AdvertApprovedDomainEvent(Guid AdvertId, string UserId) : IDomainEvent;