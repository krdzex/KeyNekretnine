using KeyNekretnine.Application.Abstraction.Email;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Adverts.Events;
using KeyNekretnine.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RejectAdvert;
internal sealed class AdvertRejectedDomainEventHandler : INotificationHandler<AdvertRejectedDomainEvent>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly IAdvertRepository _advertRepository;
    public AdvertRejectedDomainEventHandler(
        UserManager<User> userManager,
        IEmailService emailService,
        IAdvertRepository advertRepository)
    {
        _userManager = userManager;
        _emailService = emailService;
        _advertRepository = advertRepository;
    }

    public async Task Handle(AdvertRejectedDomainEvent notification, CancellationToken cancellationToken)
    {

        var user = await _userManager.FindByIdAsync(notification.UserId);

        if (user is null)
        {
            return;
        }
        var advert = await _advertRepository.GetByIdAsync(notification.AdvertId);

        if (advert is null)
        {
            return;
        }

        var sendStatus = await _emailService.SendDeclineAdvertEmail(user.Email, advert.ReferenceId, cancellationToken);

        if (!sendStatus)
        {
            return;
        }

        await Task.CompletedTask;
    }
}