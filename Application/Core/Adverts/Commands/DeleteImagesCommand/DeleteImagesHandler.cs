using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using MediatR;
using Service.Contracts;
using Shared.Error;
using System.Net;
using System.Transactions;

namespace Application.Core.Adverts.Commands.DeleteImagesCommand;
internal sealed class DeleteImagesHandler : ICommandHandler<DeleteImagesCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IServiceManager _serviceManager;

    public DeleteImagesHandler(IRepositoryManager repository, IServiceManager serviceManager)
    {
        _repository = repository;
        _serviceManager = serviceManager;
    }

    public async Task<Result<Unit>> Handle(DeleteImagesCommand request, CancellationToken cancellationToken)
    {
        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var advertExist = await _repository.Advert.ChackIfAdvertExist(request.AdvertId, cancellationToken);

            if (!advertExist)
            {
                return Result.Failure<Unit>(DomainErrors.Advert.AdvertNotFound(request.AdvertId));
            }

            var noOfImages = await _repository.Image.GetNumberOfImages(request.AdvertId, cancellationToken);

            if (noOfImages - request.ImageUrls.Count() < 0)
            {
                return Result.Failure<Unit>(DomainErrors.Advert.BadImagesCount);
            }

            var publicIds = await _repository.Image.DeleteImagesAndGetPublicIds(request.ImageUrls, request.AdvertId, cancellationToken);

            foreach (var publicId in publicIds)
            {
                var deleteResult = await _serviceManager.ImageService.DeleteImageFromCloudinary(publicId);

                if (deleteResult.StatusCode != HttpStatusCode.OK)
                {
                    return Result.Failure<Unit>(DomainErrors.ImageService.ImageCouldntBeDeleted);

                }
            }

            transactionScope.Complete();
        }

        return Unit.Value;
    }
}