﻿using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Users;
using KeyNekretnine.Domain.ValueObjects;
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
    private readonly IUserContext _userContext;

    public UpdateUserHandler(
        UserManager<User> userManager,
        IImageService imageService,
        IImageToDeleteRepository imageToDeleteRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _userManager = userManager;
        _imageService = imageService;
        _imageToDeleteRepository = imageToDeleteRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(_userContext.UserId);

        if (user is null)
        {
            return Result.Failure<Unit>(UserErrors.NotFound);
        }

        user.Update(
            UserFirstName.Create(request.FirstName),
            UserLastName.Create(request.LastName),
            request.PhoneNumber,
            About.Create(request.About));

        if (request.Image?.Length > 0)
        {
            var oldImageUrl = user.ProfileImageUrl;
            var imageUrl = await _imageService.UploadImageOnCloudinary(request.Image);

            if (oldImageUrl is not null)
            {
                await _imageToDeleteRepository.AddAsync(oldImageUrl.Value, _dateTimeProvider.Now, cancellationToken);
            }
            user.UpdateImage(ImageUrl.Create(imageUrl));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}