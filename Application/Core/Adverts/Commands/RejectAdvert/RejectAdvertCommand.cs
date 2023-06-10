using Application.Abstraction.Messaging;
using MediatR;

namespace Application.Core.Adverts.Commands.RejectAdvert;
public sealed record RejectAdvertCommand(int AdvertId) : ICommand<Unit>;

