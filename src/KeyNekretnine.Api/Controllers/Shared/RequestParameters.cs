namespace KeyNekretnine.Api.Controllers.Shared;

public abstract record RequestParameters
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 6;
    public string? OrderBy { get; init; }
}
