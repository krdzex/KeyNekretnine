namespace Contracts;
public interface IAgencyRepository
{
    Task CreateAgency(string name, string userId, CancellationToken cancellationToken);
}
