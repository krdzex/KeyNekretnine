using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.SendEmailToOwner;
public sealed record SendEmailToOwnerCommand(
    string ReferenceId,
    string FullName,
    string PhoneNumber,
    string EmailAddress,
    string Message) : ICommand;