using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RemoveAdvertPremium;
public sealed record RemoveAdvertPremiumCommand(string ReferenceId) : ICommand;
