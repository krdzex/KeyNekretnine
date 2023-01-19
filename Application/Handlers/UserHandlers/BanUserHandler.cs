using Application.Commands.UserCommands;
using Contracts;
using MediatR;

namespace Application.Handlers.UserHandlers;
internal sealed class BanUserHandler : IRequestHandler<BanUserCommand, Unit>
{
    private readonly IRepositoryManager _repository;

    public BanUserHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(BanUserCommand request, CancellationToken cancellationToken)
    {
        await _repository.User.BanUser(request.UserId, request.NoOfDays);

        return Unit.Value;
    }
}
