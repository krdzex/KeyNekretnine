using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Domain.Users.Events;
public sealed record UserBannedDomainEvent(string UserId) : IDomainEvent;