using Application.Commands.AdvertCommands;
using Contracts;
using MediatR;

namespace Application.Handlers.AdvertHandlers;
internal sealed class UpdateAdvertLocationHandler : IRequestHandler<UpdateAdvertLocationCommand, Unit>
{
    private readonly IRepositoryManager _repository;

    public UpdateAdvertLocationHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateAdvertLocationCommand request, CancellationToken cancellationToken)
    {
        await _repository.Advert.UpdateAdvertLocation(request.UpdateAdvertLocationDto, request.AdvertId, cancellationToken);

        return Unit.Value;
    }
}
