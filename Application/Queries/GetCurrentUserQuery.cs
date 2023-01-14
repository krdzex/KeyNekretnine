using MediatR;
using Shared.DataTransferObjects.User;

namespace Application.Queries;
public sealed record GetCurrentUserQuery(string Email) : IRequest<UserInformationDto>;

