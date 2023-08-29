using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;

namespace KeyNekretnine.Application.Core.Users.Commands.BanUser;
public sealed record BanUserCommand(string Email, int NoOfDays) : ICommand<Unit>;