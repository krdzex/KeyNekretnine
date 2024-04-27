using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.MakeAdvertFavorite;
public sealed record MakeAdvertFavoriteCommand(string ReferenceId) : ICommand;