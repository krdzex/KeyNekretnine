using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.Advert;

namespace KeyNekretnine.Application.Core.Adverts.Commands.CreateAdvert;
public sealed record CreateAdvertCommand(AddAdvertDto AdvertForCreating, string UserEmail) : ICommand<Unit>;