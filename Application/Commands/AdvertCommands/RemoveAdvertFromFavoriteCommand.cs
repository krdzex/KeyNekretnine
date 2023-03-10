using MediatR;

namespace Application.Commands.AdvertCommands;
public sealed record RemoveAdvertFromFavoriteCommand(int AdvertId, string UserEmail) : IRequest;

