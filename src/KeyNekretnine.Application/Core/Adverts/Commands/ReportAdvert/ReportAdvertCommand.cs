using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ReportAdvert;
public sealed record ReportAdvertCommand(string ReferenceId, string UserId, int RejectReasonId) : ICommand;