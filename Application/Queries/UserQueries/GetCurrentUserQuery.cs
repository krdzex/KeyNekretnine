using MediatR;
using Shared.DataTransferObjects.User;

namespace Application.Queries.UserQueries;
public sealed record GetCurrentUserQuery(string Email) : IRequest<UserInformationDto>;

