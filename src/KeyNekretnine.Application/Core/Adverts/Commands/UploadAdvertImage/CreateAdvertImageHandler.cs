using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.TemporeryImageDatas;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UploadAdvertImage;
internal sealed class CreateAdvertImageHandler : ICommandHandler<CreateAdvertImageCommand, CreateAdvertImageResponse>
{
    private readonly ITemporeryImageDataRepository _temporeryImageDataRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateAdvertImageHandler(
        ITemporeryImageDataRepository temporeryImageDataRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _temporeryImageDataRepository = temporeryImageDataRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<CreateAdvertImageResponse>> Handle(CreateAdvertImageCommand request, CancellationToken cancellationToken)
    {
        var uploadedImageId = await _temporeryImageDataRepository.Insert(
            request.Image,
            _dateTimeProvider.Now,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CreateAdvertImageResponse(uploadedImageId);

        return response;
    }
}