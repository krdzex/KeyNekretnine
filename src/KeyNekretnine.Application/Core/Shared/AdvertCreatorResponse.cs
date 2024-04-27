namespace KeyNekretnine.Application.Core.Shared;
public class AdvertCreatorResponse
{
    public string UserId { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string ProfileImageUrl { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string AgencyName { get; init; }
    public Guid? AgencyId { get; init; }
    public Guid? AgentId { get; init; }
    public string AgencyImageUrl { get; init; }
}
