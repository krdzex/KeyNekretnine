using Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Core.Adverts.Commands.UpdateAdvertLocation;
public sealed record UpdateAdvertLocationCommand(UpdateAdvertLocationDto UpdateAdvertLocationDto, int AdvertId) : ICommand<Unit>;