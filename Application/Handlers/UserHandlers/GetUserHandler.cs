using Application.Queries.UserQueries;
using Contracts;
using MediatR;

namespace Application.Handlers.UserHandlers;
internal sealed class GetUserHandler : IRequestHandler<GetUserQuery, Unit>
{
    private readonly IRepositoryManager _repository;

    public GetUserHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        await _repository.User.BanUser(request.UserId, request.NoOfDays);

        return Unit.Value;
    }
}
