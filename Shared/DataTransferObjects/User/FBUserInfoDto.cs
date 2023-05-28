using Newtonsoft.Json;

namespace Shared.DataTransferObjects.User;
public class FBUserInfoDto
{

    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }
}