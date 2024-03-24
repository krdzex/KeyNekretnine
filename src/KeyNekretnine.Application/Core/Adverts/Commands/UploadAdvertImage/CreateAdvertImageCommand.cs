using KeyNekretnine.Application.Abstraction.Messaging;
using Microsoft.AspNetCore.Http;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UploadAdvertImage;
public sealed record CreateAdvertImageCommand(IFormFile Image) : ICommand<CreateAdvertImageResponse>;