namespace KeyNekretnine.Application.Core.Adverts.Queries.GetBasicUpdate;
public class BasicUpdateResponse
{
    public Guid Id { get; set; }
    public DateTime? ApprovedOnDate { get; set; }
    public DateTime? RejectedOnDate { get; set; }
    public List<PropertyChange> Changes { get; set; } = new();


    public void AddChange<T>(string prop, T oldValue, T newValue)
    {
        if (!EqualityComparer<T>.Default.Equals(oldValue, newValue))
        {
            Changes.Add(new PropertyChange
            {
                Prop = prop,
                Old = oldValue,
                New = newValue
            });
        }
    }
}

public class PropertyChange
{
    public string Prop { get; set; }
    public object Old { get; set; }
    public object New { get; set; }
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
    public string DescriptionEn { get; set; }
    public string DescriptionSr { get; set; }
}