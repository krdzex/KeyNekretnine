using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using MediatR;
using Service.Contracts;
using Shared.Error;
namespace Application.Core.Users.Commands.UpdateUser;
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
        var user = await _repository.User.GetUserByEmail(request.Email);

        if (user is null)
        {
            return Result.Failure<Unit>(DomainErrors.User.UserNotFound);
        }

        if (request.UpdateUser.Image?.Length > 0)
        {
            user.ProfileImageUrl = await _serviceManager.ImageService.UploadImageOnCloudinary(request.UpdateUser.Image, request.Email.ToString());
        }

        var result = await _repository.User.UpdateUser(user, request.UpdateUser);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(error => new Error(error.Code, error.Description)).ToArray();

            return MultipleErrorsResult<Unit>.WithErrors(errors);
        }

        return Unit.Value;
    }
}