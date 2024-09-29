using KeyNekretnine.Application.Core.Adverts.Queries.GetBasicUpdate;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetLocationUpdate;

public class LocationUpdateResponse
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

public class LocationAdvertInformations
{
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public string Address { get; init; }
    public int NeighborhoodId { get; init; }
}