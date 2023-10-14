using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Domain.Agents;
public static class AgentErrors
{
    public static Error NotFound => new(
        "Agent.NotFound",
        "Agent not found");

    public static Error BadPhoneNumber => new(
        "Agent.PhoneNumber",
        "Bad phone number");
}
