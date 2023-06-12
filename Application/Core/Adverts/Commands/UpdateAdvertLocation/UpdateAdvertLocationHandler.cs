using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using MediatR;
using Shared.Error;

namespace Application.Core.Adverts.Commands.UpdateAdvertLocation;
internal sealed class UpdateAdvertLocationHandler : ICommandHandler<UpdateAdvertLocationCommand, Unit>
{
    private readonly IRepositoryManager _repository;

    public UpdateAdvertLocationHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<Unit>> Handle(UpdateAdvertLocationCommand request, CancellationToken cancellationToken)
    {
        var advertExist = await _repository.Advert.ChackIfAdvertExist(request.AdvertId, cancellationToken);

        if (!advertExist)
        {
            return Result.Failure<Unit>(DomainErrors.Advert.AdvertNotFound(request.AdvertId));
        }

        await _repository.Advert.UpdateAdvertLocation(request.UpdateAdvertLocationDto, request.AdvertId, cancellationToken);

        return Unit.Value;
    }
}
