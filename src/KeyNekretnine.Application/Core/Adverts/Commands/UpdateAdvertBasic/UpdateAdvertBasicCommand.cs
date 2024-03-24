using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertBasic;
public sealed record UpdateAdvertBasicCommand(string ReferenceId, AdvertBasicUpdateRequest UpdateData) : ICommand;
