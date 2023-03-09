using MediatR;

namespace Application.Commands.AdvertCommands;
public sealed record MakeAdvertFavoriteCommand(int AdvertId, string UserEmail) : IRequest;

