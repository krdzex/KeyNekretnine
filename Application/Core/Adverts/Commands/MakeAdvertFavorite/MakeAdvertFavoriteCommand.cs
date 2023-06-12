using Application.Abstraction.Messaging;
using MediatR;

namespace Application.Core.Adverts.Commands.MakeAdvertFavorite;
public sealed record MakeAdvertFavoriteCommand(int AdvertId, string UserEmail) : ICommand<Unit>;

