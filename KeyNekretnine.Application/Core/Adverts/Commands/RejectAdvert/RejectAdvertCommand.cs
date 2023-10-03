using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RejectAdvert;
public sealed record RejectAdvertCommand(string ReferenceId) : ICommand;