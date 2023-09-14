using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RemoveAdvertFromFavorite;
public sealed record RemoveAdvertFromFavoriteCommand(int AdvertId, string UserEmail) : ICommand<Unit>;