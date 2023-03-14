using MediatR;

namespace Application.Commands.AdvertCommands;
public sealed record ReportAdvertCommand(int AdvertId, string UserEmail, int RejectReasonId) : IRequest;
