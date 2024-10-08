using KeyNekretnine.Application.Core.Adverts.Queries.GetBasicUpdate;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetImageUpdate;
public class ImagesUpdateResponse
{
    public Guid Id { get; set; }
    public DateTime? ApprovedOnDate { get; set; }
    public DateTime? RejectedOnDate { get; set; }
    public List<PropertyChange> Changes { get; set; } = new();

    public void AddChange(string prop, string oldValue, string newValue)
    {
        if (oldValue != newValue)
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

public class ImagesInformations
{
    public List<string> Images { get; set; } = new();
}