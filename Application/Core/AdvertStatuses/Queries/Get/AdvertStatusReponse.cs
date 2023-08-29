using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.AdvertStatuses.Queries.GetAdvertStatuses;
public sealed class AdvertStatusResponse
{
    public int Id { get; init; }
    private string NameSr { get; init; }
    private string NameEn { get; init; }
    public DifferentLanguageResponse Name { get { return new DifferentLanguageResponse { Sr = NameSr, En = NameEn }; } }
}
