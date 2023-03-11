using Application.Queries.UserQueries;
using Contracts;
using MediatR;

namespace Application.Handlers.UserHandlers;
internal sealed class ConfirmUserEmailHandler : IRequestHandler<ConfirmUserEmailQuery, Unit>
{
    private readonly IRepositoryManager _repository;

    public ConfirmUserEmailHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(ConfirmUserEmailQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.User.ConfrimUserEmail(request.Token, request.Email);

        if (!result.Succeeded)
        {
            throw new ArgumentException("Error while confirming email");
        }

        return Unit.Value;
    }
}
