using Application.Abstraction.Messaging;
using MediatR;

namespace Application.Core.Adverts.Commands.RemoveAdvertFromFavorite;
public sealed record RemoveAdvertFromFavoriteCommand(int AdvertId, string UserEmail) : ICommand<Unit>;