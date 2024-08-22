using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Agents;
using MediatR;

namespace KeyNekretnine.Application.Core.Agents.Commands.RemoveAgentImage;
internal sealed class RemoveAgentImageHandler : ICommandHandler<RemoveAgentImageCommand>
{
    private readonly IAgentRepository _agentRepository;
    private readonly IImageToDeleteRepository _imageToDeleteRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    public RemoveAgentImageHandler(
        IAgentRepository agentRepository,
        IImageToDeleteRepository imageToDeleteRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _agentRepository = agentRepository;
        _imageToDeleteRepository = imageToDeleteRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(RemoveAgentImageCommand request, CancellationToken cancellationToken)
    {
        var agent = await _agentRepository.GetByIdWithAgencyAsync(request.AgentId, cancellationToken);

        if (agent is null)
        {
            return Result.Failure<Unit>(AgentErrors.NotFound);
        }

        if (agent.Agency.UserId != _userContext.UserId)
        {
            return Result.Failure<Unit>(AgencyErrors.NotOwner);
        }

        if (agent.ImageUrl != null)
        {
            await _imageToDeleteRepository.AddAsync(agent.ImageUrl.Value, _dateTimeProvider.Now, cancellationToken);

            agent.UpdateImage(null);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}