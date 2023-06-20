using Shared.DataTransferObjects.PhoneNumber;

namespace Contracts
{
    public interface IPhoneNumberRepository
    {
        Task<IEnumerable<PhoneNumberDto>> GetAll(CancellationToken cancellationToken);
    }
}
