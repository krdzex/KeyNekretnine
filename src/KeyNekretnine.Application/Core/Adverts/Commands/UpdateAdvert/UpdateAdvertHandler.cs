using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvert;
internal sealed class UpdateAdvertHandler : ICommandHandler<UpdateAdvertCommand, Unit>
{
    private readonly IRepositoryManager _repository;

    public UpdateAdvertHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<Unit>> Handle(UpdateAdvertCommand request, CancellationToken cancellationToken)
    {
        var advertExist = await _repository.Advert.ChackIfAdvertExist(request.AdvertId, cancellationToken);

        if (!advertExist)
        {
            return Result.Failure<Unit>(DomainErrors.Advert.AdvertNotFound(request.AdvertId));
        }

        await _repository.Advert.UpdateAdvertInformations(request.updateAdvertInformationsDto, request.AdvertId, cancellationToken);

        return Unit.Value;
    }
}