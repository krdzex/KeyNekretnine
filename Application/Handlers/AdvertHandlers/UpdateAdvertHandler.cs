using Application.Commands.AdvertCommands;
using Contracts;
using MediatR;

namespace Application.Handlers.AdvertHandlers;
internal sealed class UpdateAdvertHandler : IRequestHandler<UpdateAdvertCommand, Unit>
{
    private readonly IRepositoryManager _repository;

    public UpdateAdvertHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateAdvertCommand request, CancellationToken cancellationToken)
    {
        await _repository.Advert.UpdateAdvertInformations(request.updateAdvertInformationsDto, request.AdvertId, cancellationToken);

        return Unit.Value;
    }
}