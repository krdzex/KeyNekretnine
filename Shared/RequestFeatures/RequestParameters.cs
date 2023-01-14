namespace Shared.RequestFeatures;

public abstract class RequestParameters
{

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 6;
    public string? OrderBy { get; set; }

}
