using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Domain.Users.Events;
public sealed record UserCreatedDomainEvent(string UserId) : IDomainEvent;