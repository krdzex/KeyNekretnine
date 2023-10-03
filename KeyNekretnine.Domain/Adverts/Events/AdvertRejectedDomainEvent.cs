using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Domain.Adverts.Events;
public sealed record AdvertRejectedDomainEvent(Guid AdvertId, string UserId) : IDomainEvent;