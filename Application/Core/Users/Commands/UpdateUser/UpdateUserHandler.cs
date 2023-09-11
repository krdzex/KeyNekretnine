using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Shared;
using KeyNekretnine.Domain.Users;
using KeyNekretnine.Infrastructure.BackgroundJobs.ImageDeleter;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Application.Core.Users.Commands.UpdateUser;
internal sealed class UpdateUserHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IImageService _imageService;
    private readonly IImageToDeleteRepository _imageToDeleteRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateUserHandler(
        UserManager<User> userManager,
        IImageService imageService,
        IImageToDeleteRepository imageToDeleteRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _imageService = imageService;
        _imageToDeleteRepository = imageToDeleteRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
        {
            return Result.Failure<Unit>(UserErrors.NotFound);
        }

        user.Update(
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new About(request.About));

        if (request.Image?.Length > 0)
        {
            var oldImageUrl = user.ProfileImageUrl;
            var imageUrl = await _imageService.UploadImageOnCloudinary(request.Image);

            if (oldImageUrl is not null)
            {
                _imageToDeleteRepository.Add(oldImageUrl.Value, _dateTimeProvider.Now);
            }
            user.UpdateImage(new ProfileImageUrl(imageUrl));
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}