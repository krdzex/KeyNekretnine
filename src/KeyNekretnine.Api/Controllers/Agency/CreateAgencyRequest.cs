namespace KeyNekretnine.Api.Controllers.Agency;
public sealed record CreateAgencyRequest(
    string Email,
    string Password,
    string AgencyName);
