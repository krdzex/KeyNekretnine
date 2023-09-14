using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.Advert;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertLocation;
public sealed record UpdateAdvertLocationCommand(UpdateAdvertLocationDto UpdateAdvertLocationDto, int AdvertId) : ICommand<Unit>;