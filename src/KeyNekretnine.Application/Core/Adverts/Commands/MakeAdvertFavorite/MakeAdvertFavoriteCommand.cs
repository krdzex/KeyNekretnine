using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;

namespace KeyNekretnine.Application.Core.Adverts.Commands.MakeAdvertFavorite;
public sealed record MakeAdvertFavoriteCommand(int AdvertId, string UserEmail) : ICommand<Unit>;