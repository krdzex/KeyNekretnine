using MediatR;

namespace Application.Queries.UserQueries;
public sealed record ConfirmUserEmailQuery(string Token, string Email) : IRequest<bool>;

