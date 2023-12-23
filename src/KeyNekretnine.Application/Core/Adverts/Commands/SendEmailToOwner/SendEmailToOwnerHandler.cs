using KeyNekretnine.Application.Abstraction.Email;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;

namespace KeyNekretnine.Application.Core.Adverts.Commands.SendEmailToOwner;
internal sealed class SendEmailToOwnerHandler : ICommandHandler<SendEmailToOwnerCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IEmailService _emailService;

    public SendEmailToOwnerHandler(
        IAdvertRepository advertRepository,
        IEmailService emailService)
    {
        _advertRepository = advertRepository;
        _emailService = emailService;
    }

    public async Task<Result> Handle(SendEmailToOwnerCommand request, CancellationToken cancellationToken)
    {
        var advert = await _advertRepository.GetByReferenceIdWithUserAsync(request.ReferenceId, cancellationToken);

        if (advert is null)
        {
            return Result.Failure(AdvertErrors.NotFound);
        }

        var sendEmailResult = await _emailService.SendMessageToAdvertOwner(
            advert.User.Email,
            request.FullName,
            request.PhoneNumber,
            request.EmailAddress,
            request.Message,
            cancellationToken);

        if (!sendEmailResult)
        {
            return Result.Failure(new Error("Email.Error", "There was error with email service"));
        }

        return Result.Success();
    }
}