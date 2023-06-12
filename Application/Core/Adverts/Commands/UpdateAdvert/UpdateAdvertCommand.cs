using Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Core.Adverts.Commands.UpdateAdvert;
public sealed record UpdateAdvertCommand(UpdateAdvertInformationsDto updateAdvertInformationsDto, int AdvertId) : ICommand<Unit>;
