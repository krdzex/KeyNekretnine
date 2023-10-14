using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Domain.Users.Events;
public sealed record UserUnbannedDomainEvent(string UserId) : IDomainEvent;