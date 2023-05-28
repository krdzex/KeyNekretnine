using Application.Commands.UserCommands;
using Contracts;
using MediatR;
using Service.Contracts;

namespace Application.Handlers.UserHandlers;
internal sealed class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IServiceManager _serviceManager;
    public UpdateUserHandler(IRepositoryManager repository, IServiceManager serviceManager)
    {
        _repository = repository;
        _serviceManager = serviceManager;
    }
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var imageUrl = await _serviceManager.ImageService.UploadImageOnCloudinary(request.UpdateUser.Image, request.Email.ToString());

        await _repository.User.UpdateUser(request.UpdateUser, imageUrl, request.Email);

        return Unit.Value;
    }
}