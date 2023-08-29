using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ReportAdvert;
public sealed record ReportAdvertCommand(int AdvertId, string UserEmail, int RejectReasonId) : ICommand<Unit>;