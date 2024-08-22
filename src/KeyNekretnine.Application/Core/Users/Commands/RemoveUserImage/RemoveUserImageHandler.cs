using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Application.Core.Users.Commands.DeleteUserImage;
internal sealed class RemoveUserImageHandler : ICommandHandler<RemoveUserImageCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IImageToDeleteRepository _imageToDeleteRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public RemoveUserImageHandler(
        UserManager<User> userManager,
        IImageToDeleteRepository imageToDeleteRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _userManager = userManager;
        _imageToDeleteRepository = imageToDeleteRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(RemoveUserImageCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(_userContext.UserId);

        if (user is null)
        {
            return Result.Failure<Unit>(UserErrors.NotFound);
        }

        if (user.ProfileImageUrl != null)
        {
            await _imageToDeleteRepository.AddAsync(user.ProfileImageUrl.Value, _dateTimeProvider.Now, cancellationToken);

            user.UpdateImage(null);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}