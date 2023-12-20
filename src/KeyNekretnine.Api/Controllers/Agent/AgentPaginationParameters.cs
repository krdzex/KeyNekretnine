using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.Agent;

public record AgentPaginationParameters : RequestParameters
{
    public AgentPaginationParameters() => OrderBy = "firstName";
}
