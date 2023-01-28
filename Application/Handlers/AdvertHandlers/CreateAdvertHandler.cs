using Application.Commands.AdvertCommands;
using Contracts;
using MediatR;

namespace Application.Handlers.AdvertHandlers;
internal sealed class CreateAdvertHandler : IRequestHandler<CreateAdvertCommand, Unit>
{
    private readonly IRepositoryManager _repository;

    public CreateAdvertHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(CreateAdvertCommand request, CancellationToken cancellationToken)
    {
        var userId = await _repository.User.GetUserIdFromEmail(request.UserEmail);

        if (userId is null)
        {

        }
        await _repository.Advert.CreateAdvert(request.AdvertForCreating, userId);

        return Unit.Value;
    }
}

