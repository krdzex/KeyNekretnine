using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Application.Core.Users.Commands.UnbanUser;
internal sealed class UserUnbanHandler : ICommandHandler<UnbanUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    public UserUnbanHandler(UserManager<User> userManager, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(UnbanUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }

        user.UnBan();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}