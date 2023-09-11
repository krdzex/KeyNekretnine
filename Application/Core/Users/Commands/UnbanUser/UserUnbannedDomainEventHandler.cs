using Entities.Exceptions;
using KeyNekretnine.Application.Abstraction.Email;
using KeyNekretnine.Domain.Users;
using KeyNekretnine.Domain.Users.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Application.Core.Users.Commands.UnbanUser;
internal sealed class UserUnbannedDomainEventHandler : INotificationHandler<UserUnbannedDomainEvent>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public UserUnbannedDomainEventHandler(
        UserManager<User> userManager,
        IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task Handle(UserUnbannedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(notification.UserId);

        if (user is null)
        {
            return;
        }

        var result = await _emailService.SendUserUnbanEmail(user.Email, cancellationToken);

        if (!result)
        {
            throw new EmailCouldntBeSentException();
        }

        await Task.CompletedTask;
    }
}