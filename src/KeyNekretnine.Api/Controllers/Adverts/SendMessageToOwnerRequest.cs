namespace KeyNekretnine.Api.Controllers.Adverts;

public sealed record SendMessageToOwnerRequest(
    string FullName,
    string PhoneNumber,
    string SenderEmail,
    string Message);