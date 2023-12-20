using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.AgentLanguages;
using KeyNekretnine.Domain.ValueObjects;

namespace KeyNekretnine.Domain.Agents;
public class Agent : Entity
{
    private readonly List<AgentLanguage> _agentLanguages = new();

    public Agent(
        Guid id,
        AgentFirstName firstName,
        AgentLastName lastName,
        AgentPhoneNumber phoneNumber,
        Description description,
        AgentEmail email,
        SocialMedia socialMedia,
        Guid agencyId,
        DateTime createdOnTime
    )
    : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Description = description;
        Email = email;
        SocialMedia = socialMedia;
        AgencyId = agencyId;
        CretedOnTime = createdOnTime;
    }

    private Agent()
    {
    }

    public AgentFirstName FirstName { get; private set; }
    public AgentLastName LastName { get; private set; }
    public AgentPhoneNumber PhoneNumber { get; private set; }
    public ImageUrl ImageUrl { get; private set; }
    public Description Description { get; private set; }
    public AgentEmail Email { get; private set; }
    public SocialMedia SocialMedia { get; private set; }
    public Agency Agency { get; private set; }
    public Guid AgencyId { get; private set; }
    public DateTime CretedOnTime { get; private set; }
    public IReadOnlyCollection<AgentLanguage> AgentLanguages => _agentLanguages;

    public static Agent Create(
        AgentFirstName firstName,
        AgentLastName lastName,
        AgentPhoneNumber phoneNumber,
        Description description,
        AgentEmail email,
        SocialMedia socialMedia,
        Guid agencyId,
        List<int> languageIds,
        DateTime createdOnTime)
    {
        var agent = new Agent(
            Guid.NewGuid(),
            firstName,
            lastName,
            phoneNumber,
            description,
            email,
            socialMedia,
            agencyId,
            createdOnTime
            );

        if (languageIds is not null)
        {
            agent.AddLanguages(languageIds);
        }

        return agent;
    }

    public void Update(
        AgentFirstName firstName,
        AgentLastName lastName,
        AgentPhoneNumber phoneNumber,
        Description description,
        AgentEmail email,
        SocialMedia socialMedia,
        List<int> languageIds)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Description = description;
        Email = email;
        SocialMedia = socialMedia;

        if (languageIds is not null)
        {
            UpdateLanguages(languageIds);
        }
    }

    public void UpdateImage(ImageUrl imageUrl)
    {
        ImageUrl = imageUrl;
    }

    public void AddLanguages(IEnumerable<int> languageIds)
    {
        foreach (var languageId in languageIds)
        {
            _agentLanguages.Add(new AgentLanguage
            {
                LanguageId = languageId,
                AgentId = Id
            });
        }
    }

    public void UpdateLanguages(List<int> languageIds)
    {
        _agentLanguages.RemoveAll(al => !languageIds.Contains(al.LanguageId));

        foreach (var languageId in languageIds)
        {
            if (!_agentLanguages.Any(al => al.LanguageId == languageId))
            {
                _agentLanguages.Add(new AgentLanguage
                {
                    LanguageId = languageId,
                    AgentId = Id
                });
            }
        }
    }
}