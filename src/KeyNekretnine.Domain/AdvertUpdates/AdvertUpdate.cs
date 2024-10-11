using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Images;
using KeyNekretnine.Domain.ValueObjects;

namespace KeyNekretnine.Domain.AdvertUpdates;
public class AdvertUpdate : Entity
{
    public AdvertUpdate(Guid id, Guid advertId, UpdateTypes type, DateTime createdOnDate, string newContent, string oldContent)
        : base(id)
    {
        AdvertId = advertId;
        Type = type;
        CreatedOnDate = createdOnDate;
        NewContent = newContent;
        OldContent = oldContent;
    }

    private AdvertUpdate() { }

    public Guid AdvertId { get; private set; }
    public Advert Advert { get; set; }
    public DateTime? ApprovedOnDate { get; private set; }
    public UpdateTypes Type { get; private set; }
    public DateTime? RejectedOnDate { get; private set; }
    public DateTime? CreatedOnDate { get; private set; }
    public string NewContent { get; private set; }
    public string OldContent { get; private set; }

    public static AdvertUpdate Create(
    Guid advertId,
    UpdateTypes type,
    DateTime createdOnDate,
    string newContent,
    string oldContent)
    {
        var updateAgencyBasic = new AdvertUpdate(
            Guid.NewGuid(),
            advertId,
            type,
            createdOnDate,
            newContent,
            oldContent
        );

        return updateAgencyBasic;
    }
    public Result ApproveBasicUpdate(
        DateTime approvedOnDate,
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
        if (ApprovedOnDate is not null)
        {
            return Result.Failure(AdvertErrors.BasicUpdateApproved);
        }

        if (RejectedOnDate is not null)
        {
            return Result.Failure(AdvertErrors.BasicUpdateRejected);
        }

        var applyResult = Advert.ApplyBasicUpdate(
         approvedOnDate,
         price,
         floorSpace,
         noOfBedrooms,
         noOfBathrooms,
         type,
         purpose,
         yearOfBuildingCreated,
         buildingFloor,
         hasGarage,
         isFurnished,
         hasWifi,
         hasElevator,
         isUrgent,
         hasTerrace,
         isUnderConstruction,
         description);


        ApprovedOnDate = approvedOnDate;

        return Result.Success();
    }

    public Result ApproveLocationUpdate(
    DateTime approvedOnDate,
    Location location,
    int neighborhoodId)
    {
        if (ApprovedOnDate is not null)
        {
            return Result.Failure(AdvertErrors.LocationUpdateApproved);
        }

        if (RejectedOnDate is not null)
        {
            return Result.Failure(AdvertErrors.LocationUpdateRejected);
        }

        var applyResult = Advert.ApplyLocationUpdate(
         approvedOnDate,
         location,
         neighborhoodId);


        ApprovedOnDate = approvedOnDate;

        return Result.Success();
    }

    public Result ApproveFeaturesUpdate(DateTime approvedOnDate, List<string> features)
    {
        if (ApprovedOnDate is not null)
        {
            return Result.Failure(AdvertErrors.FeaturesUpdateApproved);
        }

        if (RejectedOnDate is not null)
        {
            return Result.Failure(AdvertErrors.FeaturesUpdateRejected);
        }

        Advert.ApplyFeaturesUpdate(approvedOnDate, features);

        ApprovedOnDate = approvedOnDate;

        return Result.Success();
    }

    public Result RejectFeaturesUpdate(DateTime rejectedOnDate)
    {
        if (ApprovedOnDate is not null)
        {
            return Result.Failure(AdvertErrors.FeaturesUpdateApproved);
        }

        if (RejectedOnDate is not null)
        {
            return Result.Failure(AdvertErrors.FeaturesUpdateRejected);
        }

        RejectedOnDate = rejectedOnDate;

        return Result.Success();
    }

    public Result RejectBasicUpdate(DateTime rejectedOnDate)
    {
        if (ApprovedOnDate is not null)
        {
            return Result.Failure(AdvertErrors.BasicUpdateApproved);
        }

        if (RejectedOnDate is not null)
        {
            return Result.Failure(AdvertErrors.BasicUpdateRejected);
        }

        RejectedOnDate = rejectedOnDate;

        return Result.Success();
    }

    public Result RejectLocationUpdate(DateTime rejectedOnDate)
    {
        if (ApprovedOnDate is not null)
        {
            return Result.Failure(AdvertErrors.LocationUpdateApproved);
        }

        if (RejectedOnDate is not null)
        {
            return Result.Failure(AdvertErrors.LocationUpdateRejected);
        }

        RejectedOnDate = rejectedOnDate;

        return Result.Success();
    }


    public Result ApproveImageUpdate(DateTime approvedOnDate, List<Image> images)
    {
        if (ApprovedOnDate is not null)
        {
            return Result.Failure(AdvertErrors.LocationUpdateApproved);
        }

        if (RejectedOnDate is not null)
        {
            return Result.Failure(AdvertErrors.LocationUpdateRejected);
        }

        ApprovedOnDate = approvedOnDate;

        var updateResult = Advert.ApplyImageUpdate(images, approvedOnDate);

        return updateResult;
    }
}