using KeyNekretnine.Application.Abstraction.Messaging;
using Microsoft.AspNetCore.Http;

namespace KeyNekretnine.Application.Core.Agencies.Commands.UpdateAgency;
public sealed record UpdateAgencyCommand(
    Guid AgencyId,
    string Name,
    string UserId,
    string Address,
    string Description,
    string Email,
    string WebsiteUrl,
    TimeOnly? WorkStartTime,
    TimeOnly? WorkEndTime,
    string Twitter,
    string Facebook,
    string Instagram,
    string Linkedin,
    double? Latitude,
    double? Longitude,
    List<int> LanguageIds,
    IFormFile Image) : ICommand;
