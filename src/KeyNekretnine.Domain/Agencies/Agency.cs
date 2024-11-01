﻿using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.AgencyLanguages;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.ValueObjects;

namespace KeyNekretnine.Domain.Agencies;
public class Agency : Entity
{
    private readonly List<AgencyLanguage> _agencyLanguages = new();
    private readonly List<Agent> _agents = new();

    public Agency(
    Guid id,
    AgencyName name,
    string userId,
    DateTime createdDate
        )
    : base(id)
    {
        Name = name;
        UserId = userId;
        CreatedDate = createdDate;
    }

    private Agency()
    {
    }

    public AgencyName Name { get; private set; }
    public Location Location { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public Description Description { get; private set; }
    public Email Email { get; private set; }
    public WebsiteUrl WebsiteUrl { get; private set; }
    public ImageUrl ImageUrl { get; private set; }
    public TimeRange WorkHour { get; private set; }
    public SocialMedia SocialMedia { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }

    public string UserId { get; private set; }
    public IReadOnlyCollection<AgencyLanguage> AgencyLanguages => _agencyLanguages;
    public IReadOnlyCollection<Agent> Agents => _agents;

    public static Agency Create(
        AgencyName agencyName,
        string userId,
        DateTime createdDate)
    {
        var agency = new Agency(
            Guid.NewGuid(),
            agencyName,
            userId,
            createdDate
        );

        return agency;
    }

    public void Update(
        AgencyName name,
        Location location,
        Description description,
        Email email,
        WebsiteUrl websiteUrl,
        TimeRange timeRange,
        SocialMedia socialMedia,
        PhoneNumber phoneNumber,
        List<int> languageIds)
    {
        Name = name;
        Email = email;
        Description = description;
        WebsiteUrl = websiteUrl;
        WorkHour = timeRange;
        Location = location;
        SocialMedia = socialMedia;
        PhoneNumber = phoneNumber;

        if (languageIds is not null)
        {
            UpdateLanguages(languageIds);
        }
    }

    public void UpdateLanguages(List<int> languageIds)
    {
        _agencyLanguages.RemoveAll(al => !languageIds.Contains(al.LanguageId));

        foreach (var languageId in languageIds)
        {
            if (!_agencyLanguages.Any(al => al.LanguageId == languageId))
            {
                _agencyLanguages.Add(new AgencyLanguage
                {
                    LanguageId = languageId,
                    AgencyId = Id
                });
            }
        }
    }

    public void UpdateImage(ImageUrl? imageUrl)
    {
        ImageUrl = imageUrl;
    }
}
