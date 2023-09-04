//using Entities.Exceptions;
//using MediatR;
//using Service.Contracts;

//namespace KeyNekretnine.Application.Core.Auth.Commands.UserRegistration;
//internal sealed class SendWelcomeEmailHandler : INotificationHandler<UserCreatedEvent>
//{
//    private readonly IEmailService _emailService;

//    public SendWelcomeEmailHandler(IEmailService emailService)
//    {
//        _emailService = emailService;
//    }

//    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
//    {
//        var sendResult = await _emailService.SendWelcomeEmail(notification.Email, cancellationToken);

//        if (!sendResult)
//        {
//            throw new EmailCouldntBeSentException();
//        }

//        await Task.CompletedTask;
//    }
//}