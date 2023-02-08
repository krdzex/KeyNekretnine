using MediatR;
using Shared.DataTransferObjects.Auth;

namespace Application.Notifications;
public sealed record UserSignupNotification(UserForRegistrationDto RegistrationUser) : INotification;
