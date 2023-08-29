using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;
using Service.Contracts;
using Shared.Error;
using System.Net;
using System.Transactions;

namespace KeyNekretnine.Application.Core.Users.Commands.UpdateUser;
internal sealed class UpdateUserHandler : ICommandHandler<UpdateUserCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IServiceManager _serviceManager;

    public UpdateUserHandler(IRepositoryManager repository, IServiceManager serviceManager)
    {
        _repository = repository;
        _serviceManager = serviceManager;
    }

    public async Task<Result<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var user = await _repository.User.GetUserByEmail(request.Email);

            if (user is null)
            {
                return Result.Failure<Unit>(DomainErrors.User.UserNotFound);
            }

            if (request.UpdateUser.Image?.Length > 0)
            {
                var tempProfileImageUrl = user.ProfileImageUrl;

                user.ProfileImageUrl = await _serviceManager.ImageService.UploadImageOnCloudinary(request.UpdateUser.Image);

                if (tempProfileImageUrl is not null)
                {
                    var publicId = _serviceManager.ImageService.GetPublicIdFromUrl(tempProfileImageUrl);

                    var deleteResult = await _serviceManager.ImageService.DeleteImageFromCloudinary(publicId);

                    if (deleteResult.StatusCode != HttpStatusCode.OK)
                    {
                        return Result.Failure<Unit>(DomainErrors.ImageService.ImageCouldntBeDeleted);
                    }
                }
            }

            var result = await _repository.User.UpdateUser(user, request.UpdateUser);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => new Error(error.Code, error.Description)).ToArray();

                return MultipleErrorsResult<Unit>.WithErrors(errors);
            }

            transaction.Complete();
        }
        return Unit.Value;
    }
}