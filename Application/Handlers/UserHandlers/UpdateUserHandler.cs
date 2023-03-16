using Application.Commands.UserCommands;
using Contracts;
using MediatR;

namespace Application.Handlers.UserHandlers;
internal sealed class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IRepositoryManager _repository;

    public UpdateUserHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _repository.User.UpdateUser(request.UpdateUser, request.Email);

        return Unit.Value;
    }
}