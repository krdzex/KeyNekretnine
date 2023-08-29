using Contracts;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Language;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Language.Queries.GetAllLanguages;
internal sealed class GetAllLanguagesHandler : IQueryHandler<GetAllLanguagesQuery, List<LanguageDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAllLanguagesHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<LanguageDto>>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
    {
        var languages = await _repository.Language.GetAll(cancellationToken);

        return languages.ToList();
    }
}