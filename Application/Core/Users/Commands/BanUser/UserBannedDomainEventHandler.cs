using Entities.Exceptions;
using KeyNekretnine.Application.Abstraction.Email;
using KeyNekretnine.Domain.Users;
using KeyNekretnine.Domain.Users.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Application.Core.Users.Commands.BanUser;
internal sealed class UserBannedDomainEventHandler : INotificationHandler<UserBannedDomainEvent>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public UserBannedDomainEventHandler(
        UserManager<User> userManager,
        IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }
    public async Task Handle(UserBannedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(notification.UserId);

        if (user is null)
        {
            return;
        }

        var result = await _emailService.SendUserBanEmail(user.Email, user.BanEnd, cancellationToken);

        if (!result)
        {
            throw new EmailCouldntBeSentException();
        }

        await Task.CompletedTask;
    }
}