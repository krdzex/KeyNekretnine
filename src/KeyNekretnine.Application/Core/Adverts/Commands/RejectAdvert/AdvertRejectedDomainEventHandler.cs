using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Email;
using KeyNekretnine.Domain.Adverts.Events;
using MediatR;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RejectAdvert;
internal sealed class AdvertRejectedDomainEventHandler : INotificationHandler<AdvertRejectedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public AdvertRejectedDomainEventHandler(
        IEmailService emailService,
        ISqlConnectionFactory sqlConnectionFactory)
    {
        _emailService = emailService;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task Handle(AdvertRejectedDomainEvent notification, CancellationToken cancellationToken)
    {

        using var connection = _sqlConnectionFactory.CreateConnection();

        var sql = $"""                
            SELECT 
                u.email
            FROM adverts AS a
            LEFT JOIN asp_net_users AS u ON u.id = a.user_id
            WHERE a.id = @advertId
            """
        ;

        var email = await connection.QueryFirstOrDefaultAsync<string>(sql, new { notification.AdvertId });

        if (string.IsNullOrEmpty(email))
        {
            return;
        }

        var sendStatus = await _emailService.SendRejectAdvertEmail(email, cancellationToken);

        if (!sendStatus)
        {
            return;
        }

        await Task.CompletedTask;
    }
}