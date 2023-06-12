using Microsoft.AspNetCore.Identity;

namespace Shared.DataTransferObjects
{
    public class CustomIdentityResult : IdentityResult
    {
        public string Id { get; set; }

        public CustomIdentityResult(string id)
            : base()
        {
            Id = id;
        }
    }
}
