using Application.Abstraction.Messaging;
using MediatR;

namespace Application.Core.Adverts.Commands.ReportAdvert;
public sealed record ReportAdvertCommand(int AdvertId, string UserEmail, int RejectReasonId) : ICommand<Unit>;
