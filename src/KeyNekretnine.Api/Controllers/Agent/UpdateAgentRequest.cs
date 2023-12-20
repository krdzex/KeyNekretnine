namespace KeyNekretnine.Api.Controllers.Agent;
public sealed class UpdateAgentRequest
{
    private string _firstName = null;
    private string _lastName = null;
    private string _description = null;
    private string _email = null;
    private string _twitterUrl = null;
    private string _facebookUrl = null;
    private string _instagramUrl = null;
    private string _linkedinUrl = null;
    private string _phoneNumber = null;

    public string FirstName
    {
        get => _firstName;
        init => _firstName = value ?? string.Empty;
    }

    public string LastName
    {
        get => _lastName;
        init => _lastName = value ?? string.Empty;
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

    public List<int> LanguageIds { get; init; }
    public IFormFile Image { get; init; }
}