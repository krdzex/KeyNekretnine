namespace KeyNekretnine.Api.Controllers.Agency;
public sealed record UpdateAgencyRequest(
    string AgencyName,
    string Address,
    string Description,
    string Email,
    string WebsiteUrl,
    TimeOnly? WorkStartTime,
    TimeOnly? WorkEndTime,
    string TwitterUrl,
    string FacebookUrl,
    string InstagramUrl,
    string LinkedinUrl,
    double? Latitude,
    double? Longitude,
    string PhoneNumber,
    List<int> LanguageIds,
    IFormFile Image);