namespace KeyNekretnine.Application.Core.Adverts.Queries.GetBasicUpdate;
public class BasicUpdateResponse
{
    public BasicAdvertInformations CurrentValues { get; set; }
    public BasicAdvertInformations NewValues { get; set; }
}

public class BasicAdvertInformations
{
    public int Price { get; set; }
    public int FloorSpace { get; set; }
    public int NoOfBedrooms { get; set; }
    public int NoOfBathrooms { get; set; }
    public int Type { get; set; }
    public int Purpose { get; set; }
    public int YearOfBuildingCreated { get; set; }
    public int BuildingFloor { get; set; }
    public bool HasGarage { get; set; }
    public bool IsFurnished { get; set; }
    public bool HasWifi { get; set; }
    public bool HasElevator { get; set; }
    public bool IsUrgent { get; set; }
    public bool HasTerrace { get; set; }
    public bool IsUnderConstruction { get; set; }
}