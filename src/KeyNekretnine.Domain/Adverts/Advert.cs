using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.AdvertFeatures;
using KeyNekretnine.Domain.Adverts.Events;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.Images;
using KeyNekretnine.Domain.UserAdvertReports;
using KeyNekretnine.Domain.Users;
using KeyNekretnine.Domain.ValueObjects;

namespace KeyNekretnine.Domain.Adverts;
public class Advert : Entity
{
    private readonly List<UserAdvertReport> _reports = new();
    private readonly List<AdvertFeature> _features = new();
    private readonly List<Image> _images = new();

    public Advert(
        Guid id,
        double price,
        AdvertDescription description,
        int noOfBedrooms,
        double floorSpace,
        int noOfBathrooms,
        bool hasTerrace,
        bool hasGarage,
        bool isFurnished,
        bool hasWifi,
        bool hasElevator,
        int buildingFloor,
        bool isUrgent,
        bool isUnderConstruction,
        bool isPremium,
        int? yearOfBuildingCreated,
        AdvertStatus status,
        AdvertPurpose purpose,
        AdvertType type,
        int neighborhoodId,
        Location location,
        DateTime createdTime,
        string? userId,
        Guid? agentId,
        string referenceId)
    : base(id)
    {
        Price = price;
        Description = description;
        NoOfBedrooms = noOfBedrooms;
        FloorSpace = floorSpace;
        NoOfBathrooms = noOfBathrooms;
        HasTerrace = hasTerrace;
        HasGarage = hasGarage;
        IsFurnished = isFurnished;
        HasWifi = hasWifi;
        IsUrgent = isUrgent;
        IsUnderConstruction = isUnderConstruction;
        HasElevator = hasElevator;
        BuildingFloor = buildingFloor;
        IsPremium = isPremium;
        YearOfBuildingCreated = yearOfBuildingCreated;
        Status = status;
        Purpose = purpose;
        Type = type;
        NeighborhoodId = neighborhoodId;
        Location = location;
        CreatedOnDate = createdTime;
        UserId = userId;
        AgentId = agentId;
        ReferenceId = referenceId;
    }

    private Advert()
    {
    }

    public string ReferenceId { get; private set; }

    public double Price { get; private set; }

    public AdvertDescription Description { get; private set; }

    public int NoOfBedrooms { get; private set; }

    public double FloorSpace { get; private set; }

    public int NoOfBathrooms { get; private set; }

    public bool HasTerrace { get; private set; }

    public bool HasGarage { get; private set; }

    public bool IsFurnished { get; private set; }

    public bool HasWifi { get; private set; }

    public bool HasElevator { get; private set; }

    public int BuildingFloor { get; private set; }

    public bool IsUrgent { get; private set; }

    public bool IsUnderConstruction { get; private set; }

    public bool IsPremium { get; private set; }

    public int? YearOfBuildingCreated { get; private set; }

    public AdvertStatus Status { get; private set; }

    public AdvertPurpose Purpose { get; private set; }

    public AdvertType Type { get; private set; }

    public User? User { get; private set; }
    public string? UserId { get; private set; }

    public Guid? AgentId { get; private set; }

    public int NeighborhoodId { get; private set; }

    public ImageUrl CoverImageUrl { get; private set; }

    public Location Location { get; private set; }

    public DateTime CreatedOnDate { get; private set; }

    public DateTime? ApprovedOnDate { get; private set; }

    public DateTime? RejectedOnDate { get; private set; }

    public DateTime? UpdatedOnDate { get; private set; }

    public IReadOnlyCollection<UserAdvertReport> UserAdvertReports => _reports;
    public IReadOnlyCollection<AdvertFeature> AdvertFeatures => _features;
    public IReadOnlyCollection<Image> AdvertImages => _images;


    public Result Approve(DateTime approvedDate)
    {
        if (Status == AdvertStatus.Accepted)
        {
            return Result.Failure(AdvertErrors.AlreadyAccepted);
        }

        Status = AdvertStatus.Accepted;
        ApprovedOnDate = approvedDate;

        RaiseDomainEvent(new AdvertApprovedDomainEvent(Id));
        return Result.Success();
    }

    public Result Reject(DateTime rejectedDate)
    {
        if (Status == AdvertStatus.Rejected)
        {
            return Result.Failure(AdvertErrors.AlreadyRejected);
        }

        Status = AdvertStatus.Rejected;
        RejectedOnDate = rejectedDate;

        RaiseDomainEvent(new AdvertRejectedDomainEvent(Id, UserId));
        return Result.Success();
    }

    public async Task<Result> CanCurrentUserUpdate(
        bool isAgency,
        string loggedUserId,
        IAgentRepository agentRepository
        )
    {
        if (!isAgency)
        {
            if (loggedUserId != UserId)
            {
                return Result.Failure(AdvertErrors.NotOwner);
            }
        }
        else
        {
            if (AgentId is null)
            {
                return Result.Failure(AdvertErrors.NotOwner);
            }
            var isAgentInsideLoggedAgency = await agentRepository.IsAgentInLoggedAgency(AgentId, loggedUserId);

            if (!isAgentInsideLoggedAgency)
            {
                return Result.Failure(AdvertErrors.NotOwner);
            }
        }

        return Result.Success();
    }

    public Result Report(string userId, int rejectReasonId, DateTime timeNow)
    {
        if (_reports.Any(report => report.UserId == userId && report.RejectReasonId == rejectReasonId))
        {
            return Result.Failure(Error.EmptyError);
        }

        var report = new UserAdvertReport
        {
            UserId = userId,
            AdvertId = Id,
            RejectReasonId = rejectReasonId,
            CreatedReportDate = timeNow
        };

        _reports.Add(report);

        return Result.Success();
    }

    public Result MakePremium(DateTime updatedOnDate)
    {
        if (IsPremium)
        {
            return Result.Failure(AdvertErrors.AlreadyPremium);
        }

        IsPremium = true;
        UpdatedOnDate = updatedOnDate;

        return Result.Success();
    }

    public Result RemovePremium(DateTime updatedOnDate)
    {
        if (!IsPremium)
        {
            return Result.Failure(AdvertErrors.NotPremium);
        }

        IsPremium = false;
        UpdatedOnDate = updatedOnDate;

        return Result.Success();
    }

    public Result Pause(DateTime updatedOnDate)
    {
        if (Status == AdvertStatus.Paused)
        {
            return Result.Failure(AdvertErrors.AlreadyPaused);
        }

        Status = AdvertStatus.Paused;
        UpdatedOnDate = updatedOnDate;

        return Result.Success();
    }

    public Result Activate(DateTime updatedOnDate)
    {
        if (Status != AdvertStatus.Paused)
        {
            return Result.Failure(AdvertErrors.NotPaused);
        }

        Status = AdvertStatus.Accepted;
        UpdatedOnDate = updatedOnDate;

        return Result.Success();
    }

    public Result ApplyBasicUpdate(
        DateTime updatedOnDate,
        double price,
        double floorSpace,
        int noOfBedrooms,
        int noOfBathrooms,
        int type,
        int purpose,
        int yearOfBuildingCreated,
        int buildingFloor,
        bool hasGarage,
        bool isFurnished,
        bool hasWifi,
        bool hasElevator,
        bool isUrgent,
        bool hasTerrace,
        bool isUnderConstruction,
        AdvertDescription description)
    {
        Price = price;
        FloorSpace = floorSpace;
        NoOfBedrooms = noOfBedrooms;
        NoOfBathrooms = noOfBathrooms;
        Type = (AdvertType)type;
        Purpose = (AdvertPurpose)purpose;
        YearOfBuildingCreated = yearOfBuildingCreated;
        BuildingFloor = buildingFloor;
        HasGarage = hasGarage;
        HasElevator = hasElevator;
        HasTerrace = hasTerrace;
        HasWifi = hasWifi;
        IsFurnished = isFurnished;
        IsUrgent = isUrgent;
        IsUnderConstruction = isUnderConstruction;
        UpdatedOnDate = updatedOnDate;
        Description = description;

        return Result.Success();
    }

    public Result ApplyLocationUpdate(
    DateTime updatedOnDate,
    Location location,
    int neighborhoodId
    )
    {
        UpdatedOnDate = updatedOnDate;
        Location = location;
        NeighborhoodId = neighborhoodId;

        return Result.Success();
    }

    public static Advert Create(
        double price,
        AdvertDescription description,
        int noOfBedrooms,
        double floorSpace,
        int noOfBathrooms,
        bool hasTerrace,
        bool hasGarage,
        bool isFurnished,
        bool hasWifi,
        bool hasElevator,
        int buildingFloor,
        bool isUrgent,
        bool isUnderConstruction,
        bool isPremium,
        int? yearOfBuildingCreated,
        AdvertStatus status,
        AdvertPurpose purpose,
        AdvertType type,
        int neighborhoodId,
        Location location,
        DateTime createdTime,
        string? userId,
        Guid? agentId)
    {
        var referenceId = new Random().Next().ToString("x");

        var advert = new Advert(
            Guid.NewGuid(),
            price,
            description,
            noOfBedrooms,
            floorSpace,
            noOfBathrooms,
            hasTerrace,
            hasGarage,
            isFurnished,
            hasWifi,
            hasElevator,
            buildingFloor,
            isUrgent,
            isUnderConstruction,
            isPremium,
            yearOfBuildingCreated,
            status,
            purpose,
            type,
            neighborhoodId,
            location,
            createdTime,
            userId,
            agentId,
            referenceId);

        return advert;
    }

    public void AddFeatures(List<string> features)
    {
        if (features.Count > 0)
        {
            var featuresForAdvert = features.Select(f => new AdvertFeature
            {
                Name = f,
            });

            _features.AddRange(featuresForAdvert);
        }
    }

    public void ApplyFeaturesUpdate(DateTime updatedOnDate, List<string> features)
    {
        _features.Clear();

        if (features.Count > 0)
        {
            var featuresForAdvert = features.Select(f => new AdvertFeature
            {
                Name = f,
            });

            _features.AddRange(featuresForAdvert);
        }

        UpdatedOnDate = updatedOnDate;
    }

    public Result RemoveImage(Image image, DateTime timeNow)
    {
        if (_images.Count - 1 < 2)
        {
            return Result.Failure(AdvertErrors.NoEnoughImages);
        }

        _images.Remove(image);

        UpdatedOnDate = timeNow;

        return Result.Success();
    }

    public void ChangeAgent(Guid newAgentId, DateTime timeNow)
    {
        AgentId = newAgentId;

        UpdatedOnDate = timeNow;
    }


    public Result ChangeCoverImage(string imageUrl, DateTime timeNow)
    {
        var imageToBecomeCover = _images.FirstOrDefault(img => img.Url == imageUrl);

        if (imageToBecomeCover is null)
        {
            return Result.Failure(AdvertErrors.ImageNotFound);
        }

        var tempUrl = CoverImageUrl;

        CoverImageUrl = ImageUrl.Create(imageToBecomeCover.Url);

        imageToBecomeCover.UpdateUrl(tempUrl.Value);

        UpdatedOnDate = timeNow;

        return Result.Success();
    }

    public Result ApplyImageUpdate(List<Image> images, DateTime timeNow)
    {
        if (_images.Count + images.Count > 12)
        {
            return Result.Failure(AdvertErrors.TooManyImages);
        }

        _images.AddRange(images);

        UpdatedOnDate = timeNow;

        return Result.Success();
    }
}