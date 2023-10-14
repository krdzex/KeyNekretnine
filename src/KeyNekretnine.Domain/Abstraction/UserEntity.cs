using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Domain.Abstraction;
public abstract class UserEntity : IdentityUser, IEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();


    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}