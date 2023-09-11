using Entities.Exceptions;
using KeyNekretnine.Application.Abstraction.Email;
using KeyNekretnine.Domain.Users;
using KeyNekretnine.Domain.Users.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Application.Core.Auth.Commands.UserRegistration;
internal sealed class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public UserCreatedDomainEventHandler(
        UserManager<User> userManager,
        IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(notification.UserId);

        if (user is null)
        {
            return;
        }

        var sendResult = await _emailService.SendWelcomeEmail(user.Email, cancellationToken);

        if (!sendResult)
        {
            throw new EmailCouldntBeSentException();
        }

        await Task.CompletedTask;
    }
}