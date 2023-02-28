namespace Shared.RequestFeatures;
public class UserParameters : RequestParameters
{
    public string? Username { get; set; }
    public bool? IsBanned { get; set; }
}

