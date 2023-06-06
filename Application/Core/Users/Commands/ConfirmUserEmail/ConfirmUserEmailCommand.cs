using Application.Abstraction.Messaging;

namespace Application.Core.Users.Queries.ConfirmEmailQuery;
public sealed record ConfirmUserEmailCommand(string Token, string Email) : ICommand<bool>;

