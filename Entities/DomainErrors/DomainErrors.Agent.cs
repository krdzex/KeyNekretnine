using Shared.Error;

namespace Entities.DomainErrors;
public static partial class DomainErrors
{
    public static class Agent
    {
        public static Error AgentNotFound => new Error(
            "Agent.NotFound",
            "Agent not found");
    }
}
