using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ApproveAdvert;
internal sealed class ApproveAdvertHandler : ICommandHandler<ApproveAdvertCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ApproveAdvertHandler(IAdvertRepository advertRepository, IUnitOfWork unitOfWork)
    {
        _advertRepository = advertRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ApproveAdvertCommand request, CancellationToken cancellationToken)
    {
        var advert = await _advertRepository.GetByReferenceIdAsync(request.ReferenceId, cancellationToken);

        if (advert is null)
        {
            return Result.Failure(AdvertErrors.NotFoundForAdmin);
        }

        var result = advert.Approve();

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}