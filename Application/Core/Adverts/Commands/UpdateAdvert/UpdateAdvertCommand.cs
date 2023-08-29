using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.Advert;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvert;
public sealed record UpdateAdvertCommand(UpdateAdvertInformationsDto updateAdvertInformationsDto, int AdvertId) : ICommand<Unit>;