namespace KeyNekretnine.Api.Controllers.Agency;
public sealed class UpdateAgencyRequest
{
    private string _agencyName = null;
    private string _address = null;
    private string _description = null;
    private string _email = null;
    private string _websiteUrl = null;
    private string _twitterUrl = null;
    private string _facebookUrl = null;
    private string _instagramUrl = null;
    private string _linkedinUrl = null;
    private string _phoneNumber = null;
    private double? _latitude = null;
    private double? _longitude = null;

    public string AgencyName
    {
        get => _agencyName;
        init => _agencyName = value ?? string.Empty;
    }

    public string Address
    {
        get => _address;
        init => _address = value ?? string.Empty;
    }

    public string Description
    {
        get => _description;
        init => _description = value ?? string.Empty;
    }

    public string Email
    {
        get => _email;
        init => _email = value ?? string.Empty;
    }

    public string WebsiteUrl
    {
        get => _websiteUrl;
        init => _websiteUrl = value ?? string.Empty;
    }

    public TimeOnly? WorkStartTime { get; set; }
    public TimeOnly? WorkEndTime { get; set; }

    public string TwitterUrl
    {
        get => _twitterUrl;
        init => _twitterUrl = value ?? string.Empty;
    }

    public string FacebookUrl
    {
        get => _facebookUrl;
        init => _facebookUrl = value ?? string.Empty;
    }

    public string InstagramUrl
    {
        get => _instagramUrl;
        init => _instagramUrl = value ?? string.Empty;
    }

    public string LinkedinUrl
    {
        get => _linkedinUrl;
        init => _linkedinUrl = value ?? string.Empty;
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        init => _phoneNumber = value ?? string.Empty;
    }

    public double? Latitude
    {
        get => _latitude;
        init => _latitude = value ?? 500;
    }
    public double? Longitude
    {
        get => _longitude;
        init => _longitude = value ?? 500;
    }
    public List<int> LanguageIds { get; init; }
    public IFormFile Image { get; init; }

}