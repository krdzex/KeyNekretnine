using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RejectAdvert;
public sealed record RejectAdvertCommand(int AdvertId) : ICommand<Unit>;