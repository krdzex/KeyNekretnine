namespace KeyNekretnine.Domain.Abstraction;
public interface IEntity
{
    IReadOnlyList<IDomainEvent> GetDomainEvents();

    void ClearDomainEvents();
}