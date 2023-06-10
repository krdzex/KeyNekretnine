using Application.Abstraction.Messaging;
using MediatR;

namespace Application.Core.Adverts.Commands.ApproveAdvert;
public sealed record ApproveAdvertCommand(int AdvertId) : ICommand<Unit>;
