using Application.Queries.UserQueries;
using Contracts;
using MediatR;

namespace Application.Handlers.UserHandlers;
internal sealed class ConfirmUserEmailHandler : IRequestHandler<ConfirmUserEmailQuery, bool>
{
    private readonly IRepositoryManager _repository;

    public ConfirmUserEmailHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<bool> Handle(ConfirmUserEmailQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.User.ConfrimUserEmail(request.Token, request.Email);

        return result.Succeeded;
    }
}
