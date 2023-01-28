using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Commands.AdvertCommands;
public sealed record CreateAdvertCommand(AddAdvertDto AdvertForCreating, string UserEmail) : IRequest;
