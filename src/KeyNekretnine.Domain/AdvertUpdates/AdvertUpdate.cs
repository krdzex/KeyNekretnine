using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;

namespace KeyNekretnine.Domain.AdvertUpdates;
public class AdvertUpdate : Entity
{
    public AdvertUpdate(Guid id, Guid advertId, UpdateTypes type, DateTime createdOnDate, string content)
        : base(id)
    {
        AdvertId = advertId;
        Type = type;
        CreatedOnDate = createdOnDate;
        Content = content;
    }

    private AdvertUpdate() { }

    public Guid AdvertId { get; private set; }
    public Advert Advert { get; set; }
    public DateTime? ApprovedOnDate { get; private set; }
    public UpdateTypes Type { get; private set; }
    public DateTime? RejectedOnDate { get; private set; }
    public DateTime? CreatedOnDate { get; private set; }
    public string Content { get; private set; }

    public static AdvertUpdate Create(
    Guid advertId,
    UpdateTypes type,
    DateTime createdOnDate,
    string content)
    {
        var updateAgencyBasic = new AdvertUpdate(
            Guid.NewGuid(),
            advertId,
            type,
            createdOnDate,
            content
        );

        return updateAgencyBasic;
    }
    public Result ApproveBasicUpdate(
        DateTime approvedOnDate,
        int price,
        int floorSpace,
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
        bool isUnderConstruction)
    {
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
         isUnderConstruction);


        ApprovedOnDate = approvedOnDate;

        return Result.Success();
    }
}