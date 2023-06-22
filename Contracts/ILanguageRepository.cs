using Shared.DataTransferObjects.Language;

namespace Contracts
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<LanguageDto>> GetAll(CancellationToken cancellationToken);
    }
}
