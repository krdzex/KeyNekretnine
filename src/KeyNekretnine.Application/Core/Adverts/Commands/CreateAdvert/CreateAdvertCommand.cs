using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.CreateAdvert;
public sealed record CreateAdvertCommand(CreateAdvertRequest AdvertForCreating) : ICommand;