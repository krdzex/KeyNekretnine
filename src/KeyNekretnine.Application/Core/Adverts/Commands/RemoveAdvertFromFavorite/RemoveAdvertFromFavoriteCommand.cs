using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RemoveAdvertFromFavorite;
public sealed record RemoveAdvertFromFavoriteCommand(string ReferenceId) : ICommand;