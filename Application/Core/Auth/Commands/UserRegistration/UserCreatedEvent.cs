using MediatR;

namespace Application.Core.Auth.Commands.UserRegistration
{
    public sealed record UserCreatedEvent(string Email) : INotification;

}
