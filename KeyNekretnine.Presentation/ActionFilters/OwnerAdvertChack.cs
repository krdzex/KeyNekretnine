using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace KeyNekretnine.Presentation.ActionFilters;
public class OwnerAdvertChack : IAsyncActionFilter
{
    private readonly IRepositoryManager _repositoryManager;

    public OwnerAdvertChack(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var advertId = (int)context.ActionArguments
            .SingleOrDefault(x => x.Key.ToString() == ("advertId")).Value;

        var userEmail = context.HttpContext.User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        var isOwner = await _repositoryManager.Advert.ChackIfUserIsAdvertOwner(advertId, userEmail);

        if (!isOwner)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        await next();
    }
}