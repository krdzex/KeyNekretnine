using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace KeyNekretnine.Attributes;

public class BanUserChack : IAsyncActionFilter
{

    private readonly IRepositoryManager _repositoryManager;

    public BanUserChack(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userEmail = context.HttpContext.User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        var isBanned = await _repositoryManager.User.IsUserBanned(userEmail);

        if (isBanned)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        await next();
    }
}
