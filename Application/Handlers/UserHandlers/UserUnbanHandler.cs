using Application.Commands.UserCommands;
using Contracts;
using MediatR;

namespace Application.Handlers.UserHandlers;

internal sealed class UserUnbanHandler : IRequestHandler<UnbanUserCommand, Unit>
{
    private readonly IRepositoryManager _repository;

    public UserUnbanHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(UnbanUserCommand request, CancellationToken cancellationToken)
    {
        await _repository.User.UnbanUser(request.UserId);

        return Unit.Value;
    }
}
