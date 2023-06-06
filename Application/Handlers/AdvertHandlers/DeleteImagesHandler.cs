using Application.Commands.AdvertCommands;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Service.Contracts;
using System.Transactions;

namespace Application.Handlers.AdvertHandlers;
internal sealed class DeleteImagesHandler : IRequestHandler<DeleteImagesCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IServiceManager _serviceManager;

    public DeleteImagesHandler(IRepositoryManager repository, IServiceManager serviceManager)
    {
        _repository = repository;
        _serviceManager = serviceManager;
    }

    public async Task<Unit> Handle(DeleteImagesCommand request, CancellationToken cancellationToken)
    {
        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var noOfImages = await _repository.Image.GetNumberOfImages(request.AdvertId, cancellationToken);

            if (noOfImages - request.ImageUrls.Count() < 0)
            {
                throw new BadImagesNoumberRequest();
            }

            var publicIds = await _repository.Image.DeleteImagesAndGetPublicIds(request.ImageUrls, request.AdvertId, cancellationToken);

            foreach (var publicId in publicIds)
            {
                await _serviceManager.ImageService.DeleteImageFromCloudinary(publicId);
            }

            transactionScope.Complete();
        }

        return Unit.Value;
    }
}
