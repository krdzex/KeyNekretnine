using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.AdvertTypes.Queries.Get;
public sealed class AdvertTypeResponse
{
    public int Id { get; set; }
    private string NameSr { get; init; }
    private string NameEn { get; init; }
    public DifferentLanguageResponse Name { get { return new DifferentLanguageResponse { Sr = NameSr, En = NameEn }; } }
}