using MediatR;
using Shared.DataTransferObjects.Auth;

namespace Application.Notifications.AuthNotification;
public sealed record UserSignupNotification(UserForRegistrationDto RegistrationUser) : INotification;
