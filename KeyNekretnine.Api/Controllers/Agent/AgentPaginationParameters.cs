using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.Agent;

public class AgentPaginationParameters : RequestParameters
{

    public AgentPaginationParameters() => OrderBy = "firstName";
}
