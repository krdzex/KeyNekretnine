using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ApproveAdvert;
public sealed record ApproveAdvertCommand(string ReferenceId) : ICommand;