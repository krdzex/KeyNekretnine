namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertUpdates;
public class AdvertForUpdateResponse
{
    public string ReferenceId { get; init; }
    public Guid Id { get; init; }
    public int UpdateType { get; init; }
    public DateTime CreatedOnDate { get; init; }
}
