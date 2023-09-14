using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ApproveAdvert;
public sealed record ApproveAdvertCommand(int AdvertId) : ICommand<Unit>;