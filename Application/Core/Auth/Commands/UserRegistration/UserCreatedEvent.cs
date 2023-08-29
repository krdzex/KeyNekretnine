using MediatR;

namespace KeyNekretnine.Application.Core.Auth.Commands.UserRegistration;
public sealed record UserCreatedEvent(string Email) : INotification;