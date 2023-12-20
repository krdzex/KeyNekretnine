using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.MakeAdvertPremium;
public sealed record MakeAdvertPremiumCommand(string ReferenceId) : ICommand;